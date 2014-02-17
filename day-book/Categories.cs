using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DayBook
{
    public class Category
    {
        public byte ID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public static string GetDescription(int id)
        {
            foreach (var c in CategoriesLoader.Categories)
            {
                if (id == c.ID)
                    return c.Description;
            }

            return string.Empty;
        }
    }

    public class CategoriesLoader
    {
        static private List<Category> mLst;
        static public List<Category> Categories
        {
            get
            {
                return mLst;
            }
        }

        public static List<Category> Load()
        {
            if (mLst != null)
                return mLst;

            mLst = new List<Category>();

            // parse xml configuration file
            string xmlPath = "config/Categories.xml";

            try
            {
                XDocument doc = XDocument.Load(xmlPath);
                foreach (XElement e in doc.Root.Elements())
                {
                    var category = new Category();
                    category.ID = byte.Parse(e.Attribute("id").Value);
                    category.Description = e.Attribute("description").Value;

                    mLst.Add(category);
                }
            }
            catch (Exception e)
            {
                string what = e.ToString();
            }

            return mLst;
        }
    }
}
