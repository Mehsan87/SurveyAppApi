using SurveyAppApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SurveyAppApi.Controllers
{
    public class RoadInventoriesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult RoadInventories()
        {
            string folderCreate = HttpContext.Current.Server.MapPath("~\\Images\\Roads");
            if (!Directory.Exists(folderCreate))
            {
                Directory.CreateDirectory(folderCreate);
            }

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileUpload = HttpContext.Current.Request.Files["Image"];
                var rdOwner = HttpContext.Current.Request.Form["RoadOwner_"];
                var rdType = HttpContext.Current.Request.Form["RoadType"];
                var rdName = HttpContext.Current.Request.Form["RoadName"];
                var rdLength = HttpContext.Current.Request.Form["RoadLength"];
                var rdWidth = HttpContext.Current.Request.Form["RoadWidth"];
                var rdLane = HttpContext.Current.Request.Form["NumberOfLane"];
                if (fileUpload != null)
                {
                    var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~\\Images\\Roads"), fileUpload.FileName);
                    fileUpload.SaveAs(imagePath);
                    SurveyAppDBEntities surveyAppDBEntities = new SurveyAppDBEntities();
                    surveyAppDBEntities.RoadInventories.Add(new RoadInventory
                    {
                        RoadOwner_ = rdOwner.ToString(),
                        RoadType = rdType.ToString(),
                        RoadName = rdName.ToString(),
                        RoadLength = Convert.ToDouble(rdLength),
                        RoadWidth = Convert.ToDouble(rdWidth),
                        NumberOfLane = Convert.ToInt32(rdLane),
                        ImageName = fileUpload.FileName,
                        ImagePath = imagePath.ToString()
                    });
                    surveyAppDBEntities.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
