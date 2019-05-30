using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.App.Controllers
{
    public class AlbumController : BaseController
    {
        private const string NoAvailableAlbumsConst = "There are currently no albums.";

        public IHttpResponse AllAlbums(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using (var db = new RunesDbContext())
            {
                var AllAlbums = db.Albums.ToList();

                if (AllAlbums.Count == 0)
                {
                    this.ViewData["albums"] = NoAvailableAlbumsConst;
                }
                else
                {
                    this.ViewData["albums"] = string.Join("", AllAlbums.Select(x => x.ToHtmlAll()));
                }
            }

            return this.View("All");
        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using (var db = new RunesDbContext())
            {
                var name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                var cover = ((ISet<string>)httpRequest.FormData["cover"]).FirstOrDefault();

                var album = new Album()
                {
                    Name = name,
                    Cover = cover,
                    Price = 0M,
                };

                db.Albums.Add(album);
                db.SaveChanges();
            }

            return this.Redirect("/Albums/All");
        }
        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            var albumId = int.Parse(httpRequest.QueryData["id"].ToString());

            using (var db = new RunesDbContext())
            {
                var albumFromDb = db
                    .Albums
                    .Include(x => x.Tracks)
                    .SingleOrDefault(x => x.Id == albumId);

                if(albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                this.ViewData["album"] = albumFromDb.ToHtmlDetails();
            }

            return this.View();
        }
    }
}
