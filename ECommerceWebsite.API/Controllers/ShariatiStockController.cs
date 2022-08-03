using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ECommerceWebsite.API.Models;
using System.Globalization;
using System.Data.Entity.SqlServer;

namespace ECommerceWebsite.API.Controllers
{
    public class ShariatiStockController : ApiController
    {
        private ShariatiEntities db = new ShariatiEntities();

        // GET: api/ShariatiStock
        public IQueryable<Stock> GetStock()
        {
            return db.Stock;
        }

        // GET: api/ShariatiStock/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult GetStock(string article, string size)
        {
            var stock = from s in db.Stock
                        join v in db.VW_Para
                        on s.Barcode equals v.Expr1
                        where v.Expr3 == article
                        where v.Expr4 == size
                        select new { Barcode = s.Barcode, Stock = s.Stock1, Branch = "Shariati" };


            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // GET: api/ShariatiSizesStock?article=BK2703
        [ResponseType(typeof(Stock))]
        [Route("api/ShariatiSizesStock")]
        public IHttpActionResult GetSizesStock(string article)
        {
            var stock = from s in db.Stock
                        join v in db.VW_Para
                        on s.Barcode equals v.Expr1
                        where v.Expr3 == article
                        where s.Stock1 > 0
                        select new { SizeUK = v.Expr4, Stock = s.Stock1, Branch = "Shariati" };


            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }


        [ResponseType(typeof(Stock))]
        [Route("api/ShariatiGetAvailableArticles")]
        public IHttpActionResult GetAvailableArticles()
        {
            //var stock = from s in db.Stock
            //            join v in db.VW_Para
            //            on s.Barcode equals v.Expr1
            //            where v.Expr3 == article
            //            where s.Stock1 > 0
            //            select new { SizeUK = v.Expr4, Stock = s.Stock1, Branch = "Shariati" };

            var availableArticles = (from g in db.VW_Para
                                    join s in db.Stock
                                    on g.Expr1 equals s.Barcode
                                    where s.Stock1 > 0
                                    where g.Expr3 != null
                                    select g.Expr3).Distinct();


            if (availableArticles == null)
            {
                return NotFound();
            }

            return Ok(availableArticles);
        }

        // PUT: api/ShariatiStock/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutStock(string id, Stock stock)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != stock.Barcode)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(stock).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StockExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/ShariatiFactor?article=BK2703&size=XS
        [ResponseType(typeof(Stock))]
        [Route("api/ShariatiFactor")]
        public IHttpActionResult PostStock(int factorPrice)
        {
            // ------------Create Factor------------
            decimal? code = db.Whs_QTYDocH.Max(c => c.Code) + 1;
            if (code == null) { code = 0; }

            string factorNo = db.Whs_QTYDocH.Where(d => d.TransType == 2 || d.TransType == 3).Max(d => d.DocNo);

            decimal buyerCode = db.Types.Where(t => t.value1 == "اینترنتی").Select(t => t.Counter).FirstOrDefault();

            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(DateTime.Now).ToString();
            string month = persianCalendar.GetMonth(DateTime.Now).ToString();
            if (Convert.ToInt32(month) / 10 < 1) { month = "0" + month; }
            string day = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            if (Convert.ToInt32(day) / 10 < 1) { day = "0" + day; }

            string factorDate = year + "/" + month + "/" + day;

            Whs_QTYDocH factor = new Whs_QTYDocH();
            factor.Code = (decimal)code;
            factor.DocNo = factorNo;
            factor.TransType = 2;
            factor.CompanyID = 0;
            factor.CompanyID = buyerCode;
            factor.DocDate = factorDate;
            factor.UserID = 16;  //*Check*//
            factor.DocSum = factorPrice;
            factor.Discount = 0;
            factor.DocTime = DateTime.Now.ToString("HH:mm:ss");
            factor.Sarbar = 0;
            factor.ReceivedPrice = factor.DocSum;
            factor.PayType = 0;
            factor.StationID = 1;
            factor.Note = "";
            factor.SalesType = 1;
            factor.Branch = 1;
            factor.PFishReceived = 0;
            factor.PFishPay = 0;
            factor.TAX = 0;
            factor.RoundX = 0;
            factor.RoundD = 0;
            factor.PosTypeID = 0;
            factor.PosPrice = 0;
            factor.BN = "";
            factor.P_Qty = 0;
            factor.P_Fee = 0;
            factor.VendorID = 0;
            factor.SHIFT_ID = 0;
            factor.Second_Note = "";
            factor.Chk = 0;
            factor.PrePaid = 0;
            factor.EN_YEAR = DateTime.Now.Year;
            factor.EN_Month = DateTime.Now.Month;
            factor.EN_Day = DateTime.Now.Day;

            string en_Day = factor.EN_Day.ToString();
            string en_Month = factor.EN_Month.ToString();

            if (factor.EN_Day / 10 < 1) { en_Day = "0" + factor.EN_Day.ToString(); }
            if (factor.EN_Month / 10 < 1) { en_Month = "0" + factor.EN_Month.ToString(); }

            factor.EN_DATE = en_Month + "/" + en_Day + "/" + factor.EN_YEAR;

            //factor.En_Week = (from d in db.Whs_QTYDocH
            //                  select SqlFunctions.DatePart("week", factor.EN_Month + "/" + factor.EN_Day + "/" + factor.EN_YEAR)).FirstOrDefault();   /*null*/

            factor.EN_DayofWeek = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;

            factor.En_Quarter = (int?)Math.Ceiling(Convert.ToDecimal(month) / 3);
            //factor.En_Week_Jan = ; /*null*/
            factor.EN_Year_Feb_Base = DateTime.Now.Year;
            //factor.EN_YEAR_WEEK = Convert.ToInt32((DateTime.Now.Year.ToString() + factor.En_Week.ToString()));   /*null*/
            factor.EN_Date_N = Convert.ToInt32(factor.EN_YEAR + en_Month + en_Day);

            db.Whs_QTYDocH.Add(factor);

            // ------------End of Factor------------

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Stock.Add(stock);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //if (StockExists(barcode))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }

            //return CreatedAtRoute("DefaultApi", new { id = stock.Barcode }, stock);
            return StatusCode(HttpStatusCode.OK);
        }


        [ResponseType(typeof(Stock))]
        [Route("api/ShariatiFactorDetails")]
        public IHttpActionResult PostStock(string article, string size, int qty, int mfee, int fee, int factorPrice)
        {
            decimal? code = db.Whs_QTYDocH.Max(c => c.Code);
            Whs_QTYDocH factor = db.Whs_QTYDocH.Where(p => p.DocSum == factorPrice)
                          .Where(p => p.Code == code).FirstOrDefault();

            if (code == null || factor == null) { code = 0; }

            var barcode = (from s in db.Stock
                join v in db.VW_Para
                on s.Barcode equals v.Expr1
                where v.Expr3 == article
                where v.Expr4 == size
                select s.Barcode).FirstOrDefault();

                                    // ------------Create Factor Details------------
            //Whs_QTYDocD factorToRemove = db.Whs_QTYDocD.Where(f => f.Code == code).FirstOrDefault();

            //if (factorToRemove != null) { db.Whs_QTYDocD.Remove(factorToRemove); }

            Whs_QTYDocD factorDetails = new Whs_QTYDocD();

            factorDetails.Code = code;
            factorDetails.EAN_Code = barcode;
            factorDetails.Qty = qty;
            factorDetails.MFee = mfee;
            factorDetails.Fee = fee;
            factorDetails.Discount = 0;
            factorDetails.Price = factorDetails.Fee * factorDetails.Qty;

            db.Whs_QTYDocD.Add(factorDetails);

            Stock stockToUpdate = db.Stock.Where(b => b.Barcode == barcode)
                    .Where(b => b.Branch == 1)
                    .FirstOrDefault();

            if (stockToUpdate.Export != null) { stockToUpdate.Export += factorDetails.Qty; } else { stockToUpdate.Export = factorDetails.Qty; }

            if (stockToUpdate.Stock1 != null) { stockToUpdate.Stock1 -= factorDetails.Qty; } else { stockToUpdate.Stock1 = factorDetails.Qty; }

            // ------------End of Factor Details------------

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Stock.Add(stock);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!StockExists(barcode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("DefaultApi", new { id = stock.Barcode }, stock);
            return StatusCode(HttpStatusCode.OK);
        }
        
        // DELETE: api/ShariatiStock/5
        //[ResponseType(typeof(Stock))]
        //public IHttpActionResult DeleteStock(string id)
        //{
        //    Stock stock = db.Stock.Find(id);
        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Stock.Remove(stock);
        //    db.SaveChanges();

        //    return Ok(stock);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(string id)
        {
            return db.Stock.Count(e => e.Barcode == id) > 0;
        }
    }
}