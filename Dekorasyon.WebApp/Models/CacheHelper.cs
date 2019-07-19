using BLL;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Dekorasyon.WebApp.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                CategoryManager categoryManager = new CategoryManager();
                result = categoryManager.List();

                WebCache.Set("category-cache", result, 20, true);
            }

            return result;
        }
        public static List<Post> GetPostFromCache()
        {
            var result = WebCache.Get("post-cache");

            if (result == null)
            {
                PostManager postManager = new PostManager();
                result = postManager.List();

                WebCache.Set("post-cache", result, 20, true);
            }

            return result;
        }
        public static List<Contact> GetMessageFromCache()
        {
            var result = WebCache.Get("message-cache");

            if (result == null)
            {
                ContactManager contactManager  = new ContactManager();
                result = contactManager.List();

                WebCache.Set("message-cache", result, 20, true);
            }

            return result;
        }
        public static List<About> GetAboutsFromCache()
        {
            var result = WebCache.Get("about-cache");

            if (result == null)
            {
                AboutManager aboutManager = new AboutManager();
                result = aboutManager.List();

                WebCache.Set("about-cache", result, 20, true);
            }

            return result;
        }
        public static List<Home> GetHomesFromCache()
        {
            var result = WebCache.Get("home-cache");

            if (result == null)
            {
                HomeManager homeManager = new HomeManager();
                result = homeManager.List();

                WebCache.Set("home-cache", result, 20, true);
            }

            return result;
        }
        public static void RemoveCategoriesFromCache()
        {
            Remove("category-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}