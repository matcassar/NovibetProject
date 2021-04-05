using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using IPStackDLL;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IPStackWebApi.Models;
using IPAddress = IPStackWebApi.Models.IPAddress;
using IPStackDLL.Models;
using IPStackDLL.Services;
using Microsoft.Extensions.Caching.Memory;

namespace IPStackWebApi.Controllers
{
    public class IPAddressesController : ApiController
    {
        private IPStackEntities db = new IPStackEntities();

        private readonly IPStackService _services;

        public IPAddressesController(IMemoryCache memoryCache, IPDetailsDataProvider dataProvider)
        {

            _services = new IPStackService(memoryCache, dataProvider);
        }

        // GET: api/IPAddresses
        public IQueryable<IPAddress> GetIPAddresses()
        {
            return db.IPAddresses;
        }

        [HttpGet]
        [ResponseType(typeof(IPAddress))]
        public async Task<IHttpActionResult> GetIPAddress(int id)
        {
            IPAddress iPAddress = await db.IPAddresses.FindAsync(id);
            if (iPAddress == null)
            {
                return NotFound();
            }

            return Ok(iPAddress);
        }

        // PUT: api/IPAddresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIPAddress(int id, IPAddress iPAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iPAddress.id)
            {
                return BadRequest();
            }

            db.Entry(iPAddress).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IPAddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(IPAddress))]
        public async Task<IHttpActionResult> PostIPAddress()
        {
            var ip = "141.8.56.32";
            IpStackClient ipStackClient = new IpStackClient();
            IPDetails ipDetails = ipStackClient.GetDetails(ip);

            IPAddress ipaddress = new IPAddress
            {
                ip = ipDetails.ip,
                continent_code = ipDetails.continent_code,
                continent_name = ipDetails.continent_name,
                country_code = ipDetails.country_code,
                country_name = ipDetails.country_name,
                region_code = ipDetails.region_code,
                region_name = ipDetails.region_name,
                city = ipDetails.city,
                zip = ipDetails.zip,
                latitude = ipDetails.latitude,
                longitude = ipDetails.longitude
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IPAddresses.Add(ipaddress);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IPAddressExists(ipaddress.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ipaddress.id }, ipaddress);
        }

        // DELETE: api/IPAddresses/5
        [ResponseType(typeof(IPAddress))]
        public async Task<IHttpActionResult> DeleteIPAddress(int id)
        {
            IPAddress iPAddress = await db.IPAddresses.FindAsync(id);
            if (iPAddress == null)
            {
                return NotFound();
            }

            db.IPAddresses.Remove(iPAddress);
            await db.SaveChangesAsync();

            return Ok(iPAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IPAddressExists(int id)
        {
            return db.IPAddresses.Count(e => e.id == id) > 0;
        }
    }
}