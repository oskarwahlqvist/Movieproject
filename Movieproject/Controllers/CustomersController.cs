using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movieproject.Models;
using Movieproject.Models.ViewModels;

namespace Movieproject.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult CheckOrders(string email)
        {
            List<OrderHistoryVM> OrdVmLst = new List<OrderHistoryVM>();
            if (!string.IsNullOrEmpty(email))
            {
                Customer Cust = db.Customers.Where(c => c.EmailAddress.ToLower() == email.ToLower()).FirstOrDefault();
                if (Cust != null)
                {
                    OrdVmLst = db.Orders.Where(o => o.CustomerId == Cust.Id)
                        .Join(db.OrderRows, o => o.Id, r => r.OrderId, (o, r) => new { o, r })
                        .Select(ord => new OrderHistoryVM
                        {
                            OrderId = ord.o.Id,
                            CustomerName = ord.o.Customer.FirstName + " " + ord.o.Customer.LastName,
                            MovieTitle = ord.r.Movie.Title,
                            OrderDate = ord.o.OrderDate,
                            Price = ord.r.Price,
                            TotalPrice = ord.o.TotalPrice,
                            NoofCopies = ord.r.NoofCopies

                        }).ToList();
                }
            }

            return View(OrdVmLst);
        }
        public ActionResult CheckOrdersforPV(string email)
        {
            List<OrderHistoryPVVM> OrdVmLst = new List<OrderHistoryPVVM>();
            //List<OrderHistoryPVVM> vmobjList = new List<OrderHistoryPVVM>();
            if (!String.IsNullOrEmpty(email))
            {
                Customer Cust = db.Customers.Where(c => c.EmailAddress.ToLower() == email.ToLower()).FirstOrDefault();
                
                if (Cust != null)
                {
                    ViewBag.CustomerName = Cust.FirstName + " " + Cust.LastName;
                    IEnumerable<Order> OrdersList = new List<Order>();
                    OrdersList = db.Orders.Where(o => o.CustomerId == Cust.Id).ToList();
                    foreach (var orderid in OrdersList.Select(o => o.Id).Distinct().ToList())
                    {
                        OrderHistoryPVVM vmobj = new OrderHistoryPVVM();
                        List<OrderHistoryListVM> vmlistobj = new List<OrderHistoryListVM>();
                        vmlistobj = db.Orders.Where(o => o.CustomerId == Cust.Id && o.Id == orderid)
                             .Join(db.OrderRows, o => o.Id, r => r.OrderId, (o, r) => new { o, r })
                             .Select(ord => new OrderHistoryListVM
                             {
                                 MovieTitle = ord.r.Movie.Title,
                                 Price = ord.r.Price,
                                 NoofCopies = ord.r.NoofCopies
                             }).ToList();
                        vmobj.OrderId = orderid;
                        vmobj.OrderDate = OrdersList.Where(o => o.Id == orderid).Select(o => o.OrderDate).FirstOrDefault();
                        vmobj.TotalPrice = OrdersList.Where(o => o.Id == orderid).Select(o => o.TotalPrice).FirstOrDefault();
                        vmobj.OrderHistoryList = vmlistobj;
                        OrdVmLst.Add(vmobj);
                    }


                }
            }

            return View(OrdVmLst);
        }

        public ActionResult ValidateCustomer(string email)
        {
            Session["Msg"] = "Already a Customer! ";
            Session["Valid"] = 1;
            Customer cust = db.Customers.Where(c => c.EmailAddress.ToLower() == email.ToLower()).FirstOrDefault();
            if(cust == null)
            {
                Session["Msg"] = "Not a Registered Customer! ";
                Session["valid"] = 0;
            }
            else
            {
                Session["CustId"] = cust.Id;
            }
            Session["UsrEmail"] = email;
            return View();
        }
        public ActionResult TestView()
        {
            return View("Created");
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingZip,BillingCity,DeliveryAddress,DeliverZip,DeliveryCity,EmailAddress,PhoneNo")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                Session["CustId"] = customer.Id;
                //return RedirectToAction("Index");
                ViewBag.Email = customer.EmailAddress;
                
                return View("Created");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingZip,BillingCity,DeliveryAddress,DeliverZip,DeliveryCity,EmailAddress,PhoneNo")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
