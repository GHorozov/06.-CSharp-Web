using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class TrackController : BaseController
    {
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            this.ViewData["albumId"] = httpRequest.QueryData["albumId"].ToString();
            return this.View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            var albumId = int.Parse(httpRequest.QueryData["albumId"].ToString());

            using (var db = new RunesDbContext())
            {
                var albumFromDb = db
                    .Albums
                    .SingleOrDefault(x => x.Id == albumId);

                if(albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                var name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                var link = ((ISet<string>)httpRequest.FormData["link"]).FirstOrDefault();
                var price = decimal.Parse(((ISet<string>)httpRequest.FormData["price"]).FirstOrDefault());

                var track = new Track()
                {
                    Name = name,
                    Link = link,
                    Price = price,
                };

                albumFromDb.Tracks.Add(track);
                albumFromDb.Price = (albumFromDb
                    .Tracks
                    .Select(x => x.Price)
                    .Sum() * 87) / 100;

                db.Update(albumFromDb);
                db.SaveChanges();
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }
        
        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            var albumId = int.Parse(httpRequest.QueryData["albumId"].ToString());
            var trackId = int.Parse(httpRequest.QueryData["trackId"].ToString());

            using (var db = new RunesDbContext())
            {
                var trackFromDb = db
                    .Tracks
                    .SingleOrDefault(x => x.Id == trackId);

                if (trackFromDb == null)
                {
                    return this.Redirect($"/Albums/Details?id={albumId}");
                }

                this.ViewData["albumId"] = albumId;
                this.ViewData["track"] = trackFromDb.ToHtmlDetails(albumId.ToString());
            }

            return this.View();
        }
    }
}
