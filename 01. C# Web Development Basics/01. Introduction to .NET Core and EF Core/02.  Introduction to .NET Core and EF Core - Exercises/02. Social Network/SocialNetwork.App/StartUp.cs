namespace SocialNetwork.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Data;
    using SocialNetwork.Models;
    using SocialNetwork.Models.Enums;

    public class StartUp
    {
        private const int TotalUsersCount = 20;
        private const int TotalPicturesCount = 10;
        private const int TotalAlbumCount = 10;
        private const int TotalTagsCount = 5;

        public static void Main(string[] args)
        {
            var context = new SocialNetworkDbContext();

            InitiateDatabase(context);

            SeedDatabaseUsers(context);

            SeedDatabaseAlbumAndPictures(context);

            SeedDatabaseWithTags(context);

            SeedSharePhotoAlbums(context);

            PrintAllUsersWithFriends(context);

            PrintAllUsersWithMoreThanFiveFriends(context);

            PrintAllAlbums(context);

            PrintPicturesInMoreThanTwoAlbums(context);

            PrintAllAlbumsForUserId(context);

            PrintAllAlbumsWithGivenTag(context);

            PrintAllThatHaveAlbumsWithMoreThanThreeTags(context);

            PrintAllUsersThatShareAlbumWithFriends(context);

            PrintAllAlbumsSharedWithMoreThanTwoPeople(context);

            PrintSharedAlbumsWithUserId(context);

            PrintAllAlbumsWithTheirUsers(context);

            PrintUserWithGivenName(context);

            PrintAllUsersWhoAreViewersOfAtleastOneAlbum(context);
        }

        private static void PrintAllUsersWhoAreViewersOfAtleastOneAlbum(SocialNetworkDbContext context)
        {
            var users = context
                 .Users
                 .Where(x => x.UserAlbum.Any(ua => ua.UserRole == UserRole.Viewer))
                 .Select(x => new
                 {
                     x.Username,
                     PublicAlbumsCount = x.UserAlbum.Where(a => a.UserRole == UserRole.Viewer && a.Album.IsPublic == true).Count()
                 })
                 .ToArray();

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Username} - public albums they can view: {user.PublicAlbumsCount}");
            }
        }

        private static void PrintUserWithGivenName(SocialNetworkDbContext context)
        {
            var username = "User1";

            var user = context
                .Users
                .Where(x => x.Username == username)
                .Select(x => new
                {
                    x.Username,
                    OwnerCount = x.UserAlbum.Where(a => a.UserRole == UserRole.Owner).Count(),
                    ViewerCount = x.UserAlbum.Where(a => a.UserRole == UserRole.Viewer).Count(),
                })
                .First();

            Console.WriteLine($"User: {user.Username} - Albums owner: {user.OwnerCount} - Albums viewer: {user.ViewerCount}");
        }

        private static void PrintAllAlbumsWithTheirUsers(SocialNetworkDbContext context)
        {
            var albums = context
                .Albums
                .Select(x => new
                {
                    x.Name,
                    Owner = x.User.Username,
                    AlbumUsers = x.UserAlbum.Select(ua => ua.User),
                    AlbumUsersCount = x.UserAlbum.Select(ua => ua.User).Count()
                })
                .OrderBy(x => x.Owner)
                .ThenByDescending(x => x.AlbumUsersCount)
                .ToArray();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album: {album.Name}");
                foreach (var user in album.AlbumUsers)
                {
                    var isOwner = user.Username == album.Owner ? "owner" : "viewer";
                    Console.WriteLine($"--User: {user.Username} -> {isOwner}");
                }

                Console.WriteLine("-----------------");
            }
        }

        private static void PrintSharedAlbumsWithUserId(SocialNetworkDbContext context)
        {
            var userId = 1;

            var albums = context
                .Albums
                .Where(x => x.UserAlbum.Any(ua => ua.UserId == userId && ua.UserRole == UserRole.Viewer))
                .Select(x => new
                {
                    AlbumName = x.Name,
                    AlbumPicturesCount = x.Pictures.Count
                }) 
                .OrderByDescending(x => x.AlbumPicturesCount)
                .ThenBy(x => x.AlbumName)
                .ToArray();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album name: {album.AlbumName} pictures count: {album.AlbumPicturesCount}");
            }
        }

        private static void PrintAllAlbumsSharedWithMoreThanTwoPeople(SocialNetworkDbContext context)
        {
            var albums = context
                .Albums
                .Where(x => x.UserAlbum.Where(ua => ua.UserRole == UserRole.Viewer).Count() > 2)
                .Select(x => new
                {
                    AlbumName = x.Name,
                    NumberOfPeople = x.UserAlbum.Where(ua => ua.UserRole == UserRole.Viewer).Count(),
                    Status = x.IsPublic
                })
                .OrderByDescending(x => x.NumberOfPeople)
                .ThenBy(x => x.AlbumName)
                .ToArray();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album name: {album.AlbumName} people count: {album.NumberOfPeople} IsActive: {album.Status}");
            }
        }

        private static void PrintAllUsersThatShareAlbumWithFriends(SocialNetworkDbContext context)
        {
            var users = context
                .Users
                .Where(x => x.Friends.Any())
                .Where(x => x.UserAlbum.Any())
                .Select(x => new
                {
                    Name = x.Username,
                    FriendsIds = x.Friends.Select(f => f.FriendId),
                    OwnedAlbumsIds = x.UserAlbum
                                    .Where(a => a.UserRole == UserRole.Owner)
                                    .Select(al =>al.AlbumId)
                })
                .OrderBy(x => x.Name)
                .ToArray();

            foreach (var user in users)
            {
                foreach (var friendId in user.FriendsIds)
                {
                    var currentFriend = context
                        .Users
                        .Where(x => x.Id == friendId)
                        .Include(x => x.UserAlbum)
                        .FirstOrDefault();

                    if (currentFriend != null)
                    {
                        var friendAlbums = currentFriend
                       .UserAlbum
                       .Where(x => user.OwnedAlbumsIds.Contains(x.AlbumId))
                       .ToArray();

                        if (friendAlbums.Any())
                        {
                            Console.WriteLine($"User: {user.Name}");
                            Console.WriteLine($"--Friend: {currentFriend.Username}");

                            foreach (var album in friendAlbums)
                            {
                                Console.WriteLine($"----Shared albums Id: {album.AlbumId}");
                            }

                            Console.WriteLine("------------------------------------");
                        }
                    }
                }
            }
        }

        private static void PrintAllThatHaveAlbumsWithMoreThanThreeTags(SocialNetworkDbContext context)
        {
            var albums = context
                .Albums
                .Where(x => x.Tags.Count > 3)
                .OrderByDescending(x => x.User.Albums.Count)
                .ThenByDescending(x => x.Tags.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    Owner = x.User.Username,
                    Title = x.Name,
                    Tags = x.Tags.Select(t => t.Tag.Name)
                })
                .ToArray();

            foreach (var album in albums)
            {
                Console.WriteLine($"{album.Title} - owner: {album.Owner}");
                Console.WriteLine($"--tags: {string.Join(", ", album.Tags)}");
            }
        }

        private static void PrintAllAlbumsWithGivenTag(SocialNetworkDbContext context)
        {
            var tagId = 1;

            var allTags = context.AlbumTag.ToArray();
            var albums = context
                .Albums
                .Where(x => x.Tags.Any(t => t.TagId == tagId))
                .OrderByDescending(x => x.Tags.Count)
                .Select(x => new
                {
                    x.Name,
                    Owner = x.User.Username
                })
                .OrderBy(x => x.Name)
                .ToArray();

            foreach (var album in albums)
            {
                Console.WriteLine($"{album.Name} - owner: {album.Owner}");
            }
        }

        private static void PrintAllAlbumsForUserId(SocialNetworkDbContext context)
        {
            var userId = 1;

            var albums = context
                .Albums
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.User.Username,
                    AlbumName = x.Name,
                    Pictures = x.Pictures
                        .Where(pic => pic.Album.IsPublic == true)
                        .Select(p => new
                        {
                             p.Title,
                             p.Path
                        })
                })
                .OrderBy(x => x.AlbumName)
                .ToArray();


            foreach (var album in albums)
            {
                Console.WriteLine($"{album.Username}");
                foreach (var picture in album.Pictures)
                {
                    Console.WriteLine($"--{picture.Title}");
                    Console.WriteLine($"--{picture.Path}");
                }
            }
        }

        private static void PrintPicturesInMoreThanTwoAlbums(SocialNetworkDbContext context)
        {
            var allAlbums = context.Albums.ToArray();
            var allPictures = context.Pictures.ToArray();

            var result = context
                .Pictures
                .Where(x => allPictures.Count(p => p.AlbumId == x.AlbumId) > 2)
                .Select(x => new
                {
                    x.Title,
                    AlbumNames = allAlbums
                        .Where(a => a.Id == x.AlbumId)
                        .Select(n => new
                        {
                            Names = n.Name
                        })
                        .ToArray(),
                    AlbumOwners = allAlbums
                        .Where(a => a.Id == x.AlbumId)
                        .Select(n => new
                        {
                            Owners = n.User.Username
                        })
                        .ToArray(),
                    NumberOfAlbums = allPictures.Where(pic => allPictures.Count(p => p.AlbumId == pic.AlbumId) > 2).Count()
                })
                .OrderByDescending(x => x.NumberOfAlbums)
                .ThenBy(x => x.Title)
                .ToArray();

            foreach (var picture in result)
            {
                Console.WriteLine($"{picture.Title} -> ");
                Console.WriteLine($"--{string.Join(", ", picture.AlbumNames.Select(n => n.Names))}");
                Console.WriteLine($"--{string.Join(", ", picture.AlbumOwners.Select(o => o.Owners))}");
            }
        }

        private static void PrintAllAlbums(SocialNetworkDbContext context)
        {
            var albums = context
                .Albums
                .Select(x => new
                {
                    x.Name,
                    Owner = x.User.Username,
                    PictureCount = x.Pictures.Count
                })
                .OrderByDescending(x => x.PictureCount)
                .ThenBy(x => x.Owner)
                .ToArray();

            foreach (var album in albums)
            {
                Console.WriteLine($"{album.Name} - {album.Owner} - {album.PictureCount}");
            }
        }

        private static void PrintAllUsersWithMoreThanFiveFriends(SocialNetworkDbContext context)
        {
            var currentDate = DateTime.Now;

            var users = context
                .Users
                .Where(x => x.Friends.Count > 5 && x.IsDeleted == false)
                .OrderBy(x => x.RegisteredOn)
                .ThenByDescending(x => x.Friends.Count)
                .Select(x => new
                {
                    Name = x.Username,
                    NumberOfFriends = x.Friends.Count,
                    Duration = currentDate.Subtract(x.RegisteredOn).Days
                })
                .ToArray();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} friends: {user.NumberOfFriends} registration duration: {user.Duration} days");
            }
        }

        private static void PrintAllUsersWithFriends(SocialNetworkDbContext context)
        {
            var users = context
                .Users
                .OrderByDescending(x => x.Friends.Count)
                .ThenBy(x => x.Username)
                .Select(x => new
                {
                    Name = x.Username,
                    FriendsCount = x.Friends.Count,
                    Status = x.IsDeleted
                })
                .ToArray();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} friends: {user.FriendsCount} isDeletedStatus: {user.Status}");
            }
        }

        private static void SeedSharePhotoAlbums(SocialNetworkDbContext context)
        {
            var users = context
                .Users
                .Where(x => x.Friends.Any())
                .Select(x => new
                {
                    User = x,
                    UserFriends = x.Friends.Select(f => f.Friend).ToArray(),
                    Albums = x.Albums.Select(a => a).ToArray() 
                })
                .ToArray();

            var random = new Random();

            for (int i = 0; i < users.Length; i++)
            {
                var currentUserData = users[i];
                var currentUser = currentUserData.User;
                var currentUserAlbumsData = currentUserData.Albums;

                for (int j = 0; j < currentUserAlbumsData.Count(); j++)
                {
                    var currentAlbum = currentUserAlbumsData[j];

                    try
                    {
                        var userAlbum = new UserAlbum()
                        {
                             UserId = currentUser.Id,
                             User = currentUser,
                             AlbumId = currentAlbum.Id,
                             Album = currentAlbum,
                             UserRole = UserRole.Owner,
                        };

                        currentUser.UserAlbum.Add(userAlbum);
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }

                    var randomFriendsNumber = random.Next(1, currentUserData.UserFriends.Count());

                    for (int k = 0; k < randomFriendsNumber; k++)
                    {
                        try
                        {
                            var currentFriend = currentUserData.UserFriends[k];
                            var userAlbum = new UserAlbum()
                            {
                                UserId = currentFriend.Id,
                                User = currentFriend,
                                AlbumId = currentAlbum.Id,
                                Album = currentAlbum,
                                UserRole = UserRole.Viewer
                            };

                            currentFriend.UserAlbum.Add(userAlbum);
                            context.SaveChanges();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        private static void SeedDatabaseWithTags(SocialNetworkDbContext context)
        {
            var random = new Random();

            var allAlbums = context.Albums.ToArray();
            var allTags = new List<Tag>();
            var randomAlbumCount = random.Next(1, TotalAlbumCount / 2);
            for (int i = 0; i < randomAlbumCount; i++)
            {
                var currentAlbum = allAlbums[i];

                Console.Write("Enter tag name: ");
                string input = Console.ReadLine();
                var tagName = TagTransofrmer.Transform(input);

                var tag = new Tag()
                {
                    Name = tagName + $"{i}"
                };

                var albumTag = new AlbumTag()
                {
                    AlbumId = currentAlbum.Id,
                    TagId = tag.Id
                };

                allTags.Add(tag);
                tag.Albums.Add(albumTag);
                currentAlbum.Tags.Add(albumTag);
                context.Tags.Add(tag);

                context.SaveChanges();
            }

            context.SaveChanges();
        }

        private static void SeedDatabaseAlbumAndPictures(SocialNetworkDbContext context)
        {
            var random = new Random();

            //albums
            var allUsers = context.Users.ToArray();

            for (int i = 0; i < allUsers.Count(); i++)
            {
                var currentUser = allUsers[i];

                var randomAlbumsCount = random.Next(0, TotalAlbumCount / 2);
                for (int j = 0; j < randomAlbumsCount; j++)
                {
                    var album = new Album()
                    {
                        Name = $"MyAlbum{i}",
                        BackgroundColor = $"color{i}",
                        IsPublic = true,
                        UserId = currentUser.Id
                    };

                    var randomPictureCount = random.Next(1, TotalPicturesCount);
                    for (int k = 0; k < randomPictureCount; k++)
                    {
                        var picture = new Picture()
                        {
                            Title = $"Picture{k}{i}",
                            Caption = $"Caption{k}{i}",
                            Path = $"C://pictures/{k}{i}",
                            Album = album
                        };

                        context.Pictures.Add(picture);
                        album.Pictures.Add(picture);
                        context.SaveChanges();
                    }

                    currentUser.Albums.Add(album);
                    context.SaveChanges();
                }
            }

            context.SaveChanges();
        }

        private static void SeedDatabaseUsers(SocialNetworkDbContext context)
        {
            var allUsers = new List<User>();
            for (int i = 0; i < TotalUsersCount; i++)
            {
                var user = new User()
                {
                    Username = $"User{i + 1}",
                    Password = $"aA{i}@{i}{i}",
                    Email = $"user{i}@abv.bg",
                    ProfilePicture = $"www.pictures.com/{i}",
                    RegisteredOn = DateTime.Now.AddDays(-20),
                    LastTimeLoggedIn = DateTime.Now,
                    Age = 20 + i,
                    IsDeleted = false,
                };

                allUsers.Add(user);
                context.Users.Add(user);
            }

            context.SaveChanges();

            Random random = new Random();

            foreach (var user in allUsers)
            {
                var numberOfFriends = random.Next(1, 10);

                for (int j = 0; j < numberOfFriends; j++)
                {
                    var randomFriendId = random.Next(1, TotalUsersCount);
                    if (!user.Friends.Select(x => x.FriendId).Contains(randomFriendId))
                    {
                        var userFriend = new UserFriend()
                        {
                            UserId = user.Id,
                            FriendId = randomFriendId,
                        };

                        user.Friends.Add(userFriend);
                    }
                    else
                    {
                        j--;
                    }
                }

                context.SaveChanges();
            }

            context.SaveChanges();
        }

        private static void InitiateDatabase(SocialNetworkDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
