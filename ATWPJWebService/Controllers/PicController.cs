using ATWPJWebService.Helpers;
using ATWPJWebService.Models;
using ATWPJWebService.Settings;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ATWPJWebService.Controllers
{
    [Authorize]
    public class PicController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();
        //private string fileRoot = @"C:\Photos\";

        // GET api/pic/5/0/0 - get pic by picId (zwie mal die 0 - größe nicht ändern)
        [Route("api/Pic/{Id}/{width}/{height}")]
        public HttpResponseMessage Get(int id, int width, int height)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var queryPhoto = from p in db.Photos
                             where p.Id == id && p.isDeleted == false
                             select p;

            var resultPhoto = queryPhoto.FirstOrDefault<Photo>();
            if(resultPhoto == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            //get trip owner and private stat
            var queryTrip = from t in db.Trips
                        where t.Id == resultPhoto.TripId
                        select t;

            var resultTrip = queryTrip.FirstOrDefault<Trip>();
            if(resultTrip == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            //Check User Credentials
            IdentityHelper helper = new IdentityHelper();

            //If Admin
            if(User.IsInRole("admin") == false)
            {
                //check if private
                if (resultTrip.IsPrivate == true && helper.isOwner(userId, resultTrip.UserId) == false)
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

                if (helper.isFriendOrOwner(userId, resultTrip.UserId) == false)
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

            }

            //Read File from Disk
            try
            {
                MemoryStream ms = new MemoryStream();
                using (FileStream file = new FileStream(ApplicationSettings.FileRootDirectory + resultPhoto.FileName, FileMode.Open, FileAccess.Read))
                {
                    if (width != 0 && height != 0)
                    {
                        //resize image
                        var myImage = Image.FromStream(file);
                        PictureHelper pHelper = new PictureHelper();
                        var bitmap = pHelper.ResizeImage(myImage, width, height);
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        file.CopyTo(ms);
                    }
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                return result;
            }

            catch (Exception)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // POST api/pic - add new pic
        [Route("api/Pic/{tripId}/{latitude}/{longitude}")]
        public async Task<HttpResponseMessage> Post(int tripId, string latitude, string longitude)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //get trip owner and private stat
            var queryTrip = from t in db.Trips
                            where t.Id == tripId
                            select t;

            var resultTrip = queryTrip.FirstOrDefault<Trip>();
            if (resultTrip == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            //Check User Credentials
            IdentityHelper helper = new IdentityHelper();

            //check if owner
            if (helper.isOwner(userId, resultTrip.UserId) == false)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 10 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            //generate Filename
                            string fileName = Path.GetRandomFileName();
                            fileName = Path.GetFileNameWithoutExtension(fileName);

                            //var filePath = fileRoot + fileName + extension;
                            var filePath = ApplicationSettings.FileRootDirectory + fileName + extension;
                            postedFile.SaveAs(filePath);

                            //Save Data to Database
                            db.Photos.Add(new Photo()
                            {
                                FileName = fileName + extension,
                                TripId = tripId,
                                Latitude = double.Parse(latitude.Replace(',', '.'), CultureInfo.InvariantCulture),
                                Longitude = double.Parse(longitude.Replace(',', '.'), CultureInfo.InvariantCulture)
                            });

                            db.SaveChanges();
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        // DELETE api/pic/5
        public HttpResponseMessage Delete(int id)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var queryPhoto = from p in db.Photos
                             where p.Id == id && p.isDeleted == false
                             select p;

            var resultPhoto = queryPhoto.FirstOrDefault<Photo>();
            if (resultPhoto == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            //get trip owner and private stat
            var queryTrip = from t in db.Trips
                            where t.Id == resultPhoto.TripId
                            select t;

            var resultTrip = queryTrip.FirstOrDefault<Trip>();
            if (resultTrip == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            //Check User Credentials
            IdentityHelper helper = new IdentityHelper();

            //check if owner
            if (helper.isOwner(userId, resultTrip.UserId) == false)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            resultPhoto.isDeleted = true;
            db.SaveChanges();

            try
            {
                File.Delete(ApplicationSettings.FileRootDirectory + resultPhoto.FileName);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
