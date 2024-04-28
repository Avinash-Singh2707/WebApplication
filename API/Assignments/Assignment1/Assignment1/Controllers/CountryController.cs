using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Assignment1.Models;   // Adding models

namespace Assignment1.Controllers
{
    public class CountryController : ApiController
    {
        static List<Country> countrylist = new List<Country>()
        {
            new Country{ID=1,CountryName="India",Capital="New Delhi"},
            new Country{ID=2,CountryName="France",Capital="Paris"},
            new Country{ID=3,CountryName="UK",Capital="London"},
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<Country> Get()
        {
            return countrylist;
        }
        [HttpGet]            // Read
        [Route("Bymessage")]
        public HttpResponseMessage GetAllCountry()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, countrylist);
            return response;
        }

        [HttpPost]      // create..
        [Route("AllPost")]
        public List<Country> PostAll([FromBody] Country country)
        {
            countrylist.Add(country);
            return countrylist;
        }
        [HttpPost]
        [Route("countrypost")]
        public void CountryPost([FromUri] int Id,string countryname, string captial)
        {
            Country country = new Country();
            country.ID = Id;
            country.CountryName = countryname;
            country.Capital = captial;
            countrylist.Add(country);
        }



        [HttpPut]
        public void Put(int cid,[FromUri] Country c)
        {
            countrylist[cid - 1] = c;
        }

        [HttpDelete]
        public void Delete(int cid)
        {
            countrylist.RemoveAt(cid - 1);
        }
    }
}
