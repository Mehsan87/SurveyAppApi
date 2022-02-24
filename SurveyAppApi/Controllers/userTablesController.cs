using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SurveyAppApi.Models;

namespace SurveyAppApi.Controllers
{
    public class userTablesController : ApiController
    {
        private SurveyAppDBEntities db = new SurveyAppDBEntities();

        // GET: api/userTables
        public IQueryable<UserTable> GetUserTables()
        {
            return db.UserTables;
        }

        // GET: api/userTables/5
        [ResponseType(typeof(UserTable))]
        public async Task<IHttpActionResult> GetUserTable(int id)
        {
            UserTable userTable = await db.UserTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }

            return Ok(userTable);
        }
        
        // POST: api/userTables
        [ResponseType(typeof(UserTable))]
        public async Task<IHttpActionResult> PostUserTable(UserTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTables.Add(userTable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userTable.Id }, userTable);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTableExists(int id)
        {
            return db.UserTables.Count(e => e.Id == id) > 0;
        }
    }
}