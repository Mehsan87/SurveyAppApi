using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;
using SurveyAppApi.Models;

namespace SurveyAppApi.Controllers
{
    public class BuildingInventoriesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult BuildingInventories()
        {
            string folderCreate = HttpContext.Current.Server.MapPath("~\\Images\\Buildings");
            if (!Directory.Exists(folderCreate))
            {
                Directory.CreateDirectory(folderCreate);
            }

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileUpload = HttpContext.Current.Request.Files["Image"];
                var bldType = HttpContext.Current.Request.Form["BuildingType"];
                var bldUse = HttpContext.Current.Request.Form["BuildingUse"];
                var bldName = HttpContext.Current.Request.Form["BuildingName"];
                var bldLength = HttpContext.Current.Request.Form["BuildingLength"];
                var bldWidth = HttpContext.Current.Request.Form["BuildingWidth"];
                var bldFloor = HttpContext.Current.Request.Form["NumberOfFloor"];
                if (fileUpload != null)
                {
                    var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~\\Images\\Buildings"), fileUpload.FileName);
                    fileUpload.SaveAs(imagePath);
                    SurveyAppDBEntities surveyAppDBEntities = new SurveyAppDBEntities();
                    surveyAppDBEntities.BuildingInventories.Add(new BuildingInventory
                    {
                        BuildingType = bldType.ToString(),
                        BuildingUse = bldUse.ToString(),
                        BuildingName = bldName.ToString(),
                        BuildingLength = Convert.ToDouble(bldLength),
                        BuildingWidth = Convert.ToDouble(bldWidth),
                        NumberOfFloor = Convert.ToInt32(bldFloor),
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
