using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Movieproject.Models;
using Movieproject.Models.ViewModels;

namespace Movieproject.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            return View(db.Movies.ToList());
        }
        public JsonResult FamousMovie()
        {
            //var movies = db.Movies
            //    .Join(db.OrderRows, m => m.Id, r => r.MovieId, (m, r) => new { m, r })
            //    .GroupBy(mr => mr.r.MovieId)
            //    .Select(mr => new
            //    {
            //        Id = mr.Key,
            //        count = mr.Count()
            //    }).OrderByDescending(mr => mr.count).ToList().Take(1);

            //var mov = db.Movies.Find(movies.FirstOrDefault().Id);
            //return Json(movies, JsonRequestBehavior.AllowGet);

            //ViewBag.Oskar = mov.Title;
            //ViewBag.Oskar2 = mov.ImageUrl;

            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------

            List<Movie> TopfiveList = new List<Movie>();
            var movies = db.Movies
                .Join(db.OrderRows, m => m.Id, r => r.MovieId, (m, r) => new { m, r })
                .GroupBy(mr => mr.r.MovieId)
                .Select(mr => new
                {
                    Id = mr.Key,
                    count = mr.Count()
                }).OrderByDescending(mr => mr.count).ToList().Take(5);
            foreach(var mov in movies) {
                TopfiveList.Add(db.Movies.Find(mov.Id));
            }
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult HomePageTest()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

            List<Movie> MovieTopFive = new List<Movie>();
            var movies = db.Movies
                .Join(db.OrderRows, m => m.Id, r => r.MovieId, (m, r) => new { m, r })
                .GroupBy(mr => mr.r.MovieId)
                .Select(mr => new
                {
                    Id = mr.Key,
                    count = mr.Count()
                }).OrderByDescending(mr => mr.count).ToList().Take(5);
            foreach (var mov in movies)
            {
                MovieTopFive.Add(db.Movies.Find(mov.Id));
            }

            //var MovieTitleList = db.Movies.OrderBy(m => m.Title).ToList().Take(5);
            var MovieNewestList = db.Movies.OrderByDescending(m => m.ReleaseYear).ToList().Take(5);
            var MovieOldestList = db.Movies.OrderBy(m => m.ReleaseYear).ToList().Take(5);
            var MovieCheapestList = db.Movies.OrderBy(m => m.Price).ToList().Take(5);

            //var MostExpensiveOldOne = db.Orders.OrderByDescending(o => o.TotalPrice).ToList().Take(1);
            //var MostExpensiveOldOne = db.Orders.OrderByDescending(o => o.TotalPrice).ToList().Take(1).FirstOrDefault();
            
            var MostExpensiveOrder = db.Orders.OrderByDescending(o => o.TotalPrice).FirstOrDefault();
            var MostExpensiveOrderID = db.Customers.Where(c => c.Id == MostExpensiveOrder.CustomerId).FirstOrDefault();
            var CustomerName = MostExpensiveOrderID.FirstName + " " + MostExpensiveOrderID.LastName;
            ViewBag.CustName2 = CustomerName;
            

            var tupleModel = new Tuple<List<Movie>, List<Movie>, List<Movie>, List<Movie>>(MovieTopFive.ToList(), MovieNewestList.ToList(), MovieOldestList.ToList(), MovieCheapestList.ToList());

            return View(tupleModel);
        }

        [AllowAnonymous]
        public ActionResult HomePage()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

            List<Movie> MovieTopFive = new List<Movie>();
            var movies = db.Movies
                .Join(db.OrderRows, m => m.Id, r => r.MovieId, (m, r) => new { m, r })
                .GroupBy(mr => mr.r.MovieId)
                .Select(mr => new
                {
                    Id = mr.Key,
                    count = mr.Count()
                }).OrderByDescending(mr => mr.count).ToList().Take(5);
            foreach (var mov in movies)
            {
                MovieTopFive.Add(db.Movies.Find(mov.Id));
            }

            //var MovieTitleList = db.Movies.OrderBy(m => m.Title).ToList().Take(5);
            var MovieNewestList = db.Movies.OrderByDescending(m => m.ReleaseYear).ToList().Take(5);
            var MovieOldestList = db.Movies.OrderBy(m => m.ReleaseYear).ToList().Take(5);
            var MovieCheapestList = db.Movies.OrderBy(m => m.Price).ToList().Take(5);

            //var MostExpensiveOldOne = db.Orders.OrderByDescending(o => o.TotalPrice).ToList().Take(1);
            //var MostExpensiveOldOne = db.Orders.OrderByDescending(o => o.TotalPrice).ToList().Take(1).FirstOrDefault();

            var MostExpensiveOrder = db.Orders.OrderByDescending(o => o.TotalPrice).FirstOrDefault();
            var MostExpensiveOrderID = db.Customers.Where(c => c.Id == MostExpensiveOrder.CustomerId).FirstOrDefault();
            var CustomerName = MostExpensiveOrderID.FirstName + " " + MostExpensiveOrderID.LastName;
            ViewBag.CustName2 = CustomerName;


            var tupleModel = new Tuple<List<Movie>, List<Movie>, List<Movie>, List<Movie>>(MovieTopFive.ToList(), MovieNewestList.ToList(), MovieOldestList.ToList(), MovieCheapestList.ToList());

            return View(tupleModel);
        }
        [AllowAnonymous]
        public ActionResult HomePageOld()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            //var PopularMovies = db.OrderRows.OrderByDescending(o => o.MovieId).ToList().Count().ToString().Take(1);
            //var pop = db.OrderRows.OrderByDescending(o => o.MovieId).GroupBy(o => o.MovieId).ToList().Take(1);
            //var pop3 = db.OrderRows.GroupBy(o => o.MovieId).Count();
            ////var pop2 = db.OrderRows.Select(Where
            ////var popppp = db.OrderRows.Count(MovieId)

            List<Movie> TopfiveList = new List<Movie>();
            var movies = db.Movies
                .Join(db.OrderRows, m => m.Id, r => r.MovieId, (m, r) => new { m, r })
                .GroupBy(mr => mr.r.MovieId)
                .Select(mr => new
                {
                    Id = mr.Key,
                    count = mr.Count()
                }).OrderByDescending(mr => mr.count).ToList().Take(5);
            foreach (var mov in movies)
            {
                TopfiveList.Add(db.Movies.Find(mov.Id));
            }

            var MovieList = db.Movies.OrderBy(m => m.Title).ToList().Take(5);
            var MovieList2 = db.Movies.OrderBy(m => m.ReleaseYear).ToList().Take(5);
            var MovieList3 = db.Movies.OrderBy(m => m.Price).ToList().Take(5);
            var DyrastOrder = db.Orders.OrderByDescending(o => o.TotalPrice).ToList().Take(1);

            //var DyrastOrder2 = db.Orders.OrderByDescending(o => o.TotalPrice).ToList().Take(1).FirstOrDefault();
            var DyrastOrder2 = db.Orders.OrderByDescending(o => o.TotalPrice).FirstOrDefault();

            var DyrastCustomer = db.Customers.Where(c => c.Id == DyrastOrder2.CustomerId).FirstOrDefault();
            var CustomerName = DyrastCustomer.FirstName + " " + DyrastCustomer.LastName;
            ViewBag.CustName2 = CustomerName;

            var tupleModel = new Tuple<List<Movie>, List<Movie>, List<Movie>, List<Order>, List<Movie>>(MovieList.ToList(), MovieList2.ToList(), MovieList3.ToList(), DyrastOrder.ToList(), TopfiveList.ToList());
            return View(tupleModel);
        }

        [AllowAnonymous]
        public ActionResult MoviesList()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            if (Session["search"] != null) Session["PrevSearch"] = Session["Search"]; //Remembers the search
            Session.Remove("Search");
            return View(db.Movies.ToList());
        }
        [AllowAnonymous]
        public ActionResult SearchMovie()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            if (Session["PrevSearch"] != null) Session["Search"] = Session["PrevSearch"]; //Remembers the search
            return View();
        }
        [AllowAnonymous]
        public ActionResult FilterMoviesList(string srchtxt)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            List<Movie>movieslist = new List<Movie>();
            Session["Search"] = srchtxt;
            //var movieslist = db.Movies.Where(m => m.Title.Contains(srchtxt));
            if (string.IsNullOrEmpty(srchtxt))
            {
                ViewBag.Flag = 1;
                ViewBag.Count = 0;
            }
            else
            {
                movieslist = db.Movies.Where(m => m.Title.Contains(srchtxt)).ToList();
                ViewBag.Flag = 0;
                ViewBag.Count = movieslist.Count();
            }
            return View("MoviesList", movieslist);
        }
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public ActionResult PageInationMovies(int page =1)
        {
            ViewBag.Url = TempData["Url"];
            if (Session["search"] != null) Session["PrevSearch"] = Session["Search"]; //Remembers the search
            Session.Remove("Search");
            Session["CurrentPage"] = page;
            PageinationVM paginateobj = new PageinationVM();
            paginateobj.CurrentPage = page;
            paginateobj.MoviesPerPage = 6;
            paginateobj.Movies = CurrentPageMovies(paginateobj.CurrentPage, paginateobj.MoviesPerPage);
            paginateobj.MoviesPerRow = 3;
            paginateobj.PageCount = PageinationVM.TotalPages(db.Movies.Count(), paginateobj.MoviesPerPage);
            return View(paginateobj);
        }
        public IEnumerable<Movie> CurrentPageMovies(int CurrentPage, int CountPerPage)
        {
            int startcount = (CurrentPage -1) * CountPerPage;
            var movieslist = db.Movies.OrderBy(m => m.Id);
            return movieslist.Skip(startcount).Take(CountPerPage); 
        }
        [AllowAnonymous]
        public ActionResult AddtoCart(int MovieId,int AddorRemove, int IsCart)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            List<int> MovieIdList = new List<int>();
            if (Session["MoviesList"] != null) MovieIdList = (List<int>)Session["MoviesList"];
           
            if (AddorRemove == 1)
            {
                MovieIdList.Add(MovieId);
            }
            else if (AddorRemove == 0) 
            {
                MovieIdList.Reverse();
                MovieIdList.Remove(MovieId);
                MovieIdList.Reverse();
            }      
            Session["MoviesList"] = MovieIdList;
            Session["MoviesCount"] = MovieIdList.Count;
            if (MovieIdList.Count == 0) return View("NoItems");
            if (IsCart == 0) return RedirectToAction("MoviesList");
            if (IsCart == 2) return RedirectToAction("SearchMovie");
            if (IsCart == 3) 
            {
                TempData["Url"] = "https://localhost:44316/Movies/PageInationMovies?page=" + Session["CurrentPage"];
                return RedirectToAction("PageInationMovies", new { page = Session["CurrentPage"] });
            }
            return RedirectToAction("DisplayCart",new { IsOrder = 0 });

        }
        public ActionResult CreateOrder( int IsVerified)
        {
            //SelectedMovIdList.Clear; 

            return View();
        }
        [AllowAnonymous]
        public ActionResult DisplayCart(int IsOrder)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            List<int> NoofCopiesList = new List<int>();
            List<decimal> PriceList = new List<decimal>();

            Session["IsOrder"] = IsOrder;
            if (Session["MoviesList"] == null || (int)Session["MoviesCount"] == 0) return View("NoItems");
            List<int> SelectedMovIdList = new List<int>();
            List<int> DistinctMovIdList = new List<int>();
            List<MovieListVM> MoviesVMList = new List<MovieListVM>();
            MovieListVM vmobj;
            decimal total = 0;
            
            SelectedMovIdList = (List<int>)Session["MoviesList"];
            foreach(int id in SelectedMovIdList)
            {
                if (!DistinctMovIdList.Contains(id))
                {
                    DistinctMovIdList.Add(id);
                }
            }
            foreach(int Id in DistinctMovIdList)
            {
                vmobj = new MovieListVM();

                var movie = db.Movies.Find(Id);
                int copies = SelectedMovIdList.Count(m => m == Id);
                vmobj.MovieId = movie.Id;
                vmobj.Movie = movie.Title;
                vmobj.ReleaseYear = movie.ReleaseYear;
                vmobj.NoofCopies = copies;
                vmobj.Price = movie.Price * copies;
                total += vmobj.Price;
                NoofCopiesList.Add(vmobj.NoofCopies);
                PriceList.Add(vmobj.Price);
                MoviesVMList.Add(vmobj);

            }
            ViewBag.TotalPrice = total;
            Session["MovieIdList"] = DistinctMovIdList;
            Session["CopiesList"] = NoofCopiesList;
            Session["PriceList"] = PriceList;
            Session["TotalPrice"] = total;

            return View(MoviesVMList);
            
        }
        //public ActionResult TestView()
        //{
        //    return View("Created","Customers");
        //}
        [AllowAnonymous]
        public ActionResult SubmitOrder()
        {
            List<int> MovieIdList = new List<int>();
            List<int> CopiesList = new List<int>();
            List<decimal> PriceList = new List<decimal>();
            int idx = 0;

            Order ord = new Order();
            ord.CustomerId = (int)Session["CustId"];
            ord.OrderDate = DateTime.Now;
            ord.TotalPrice = (decimal)Session["TotalPrice"];
            db.Orders.Add(ord);
            db.SaveChanges();
            //var OrdId = ord.Id;
            //var OrdId = db.Orders.OrderByDescending(o => o.Id).Take(1).Select(o => o.Id);
            //var OrdId = db.Orders.Where(o => o.CustomerId == (int)Session["CustId"]).OrderByDescending(o => o.Id).Select(o => o.Id).Take(1);

            MovieIdList = (List<int>)Session["MovieIdList"];
            CopiesList = (List<int>)Session["CopiesList"];
            PriceList = (List<decimal>)Session["PriceList"];

            foreach (var movieId in MovieIdList)
            {
                OrderRow ordrw = new OrderRow();
                ordrw.OrderId = ord.Id;
                ordrw.MovieId = movieId;
                ordrw.NoofCopies = CopiesList.ElementAt(idx);
                ordrw.Price = PriceList.ElementAt(idx);
                db.OrderRows.Add(ordrw);
                db.SaveChanges();
                idx += 1;
            }
            ViewBag.OrderId = ord.Id;
            Session.Clear();
            //Session.Remove("NameofSession");
            return View();
        }
        // GET: Movies/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Director,ReleaseYear,LeadActor,Price,ImageUrl")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Director,ReleaseYear,LeadActor,Price,ImageUrl")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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

        ////Session
        //public string VariableDemo()
        //{
        //    Session["Name"] = "Sruthi";
        //    return Session["Name"].ToString();
        //}
        ////Session
        //public string DisplaySession()
        //{
        //    if (Session["Name"] != null) return Session["Name"].ToString();
        //    else return "No Session data available";
        //}


        ////Cookie
        //public string VariableDemo2()
        //{
        //    Session["Name"] = "Sruthi";
        //    HttpCookie cookie = new HttpCookie("LastName");
        //        cookie.Value = "fgdfg";
        //    Response.Cookies.Add(cookie);
        //    Response.Cookies["FirstName"].Value = "Oskar";
        //        return Session["Name"].ToString();


        //}
        //public string DisplayCookie()
        //{
        //    string str = "Cookie Data: ";
        //    if (Request.Cookies.AllKeys.Contains("LastName")) 
        //        str += "Last Name: " + Request.Cookies["LastName"].Value + ",";
        //    if (Request.Cookies.AllKeys.Contains("FirstName")) 
        //        str += "FirstName: " + Request.Cookies["FirstName"].Value;
        //    return str;
        //}



    }
}
