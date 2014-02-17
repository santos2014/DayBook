using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DayBook
{
    public class SubCategory
    {
        public byte ID
        {
            get;
            set;
        }

        public int MainCategroy
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public void Reset()
        {
            Description = "";
            MainCategroy = 0;
            ID = 0;
        }
    }

    public class SubCategoriesLoader
    {
        static private List<SubCategory> mLst;

        public static List<SubCategory> Load()
        {
            if (mLst != null)
                return mLst;

            mLst = new List<SubCategory>();

            // parse xml configuration file
            string xmlPath = "config/SubCategories.xml";

            try
            {
                XDocument doc = XDocument.Load(xmlPath);
                foreach (XElement e in doc.Root.Elements())
                {
                    var sc = new SubCategory();
                    sc.ID = byte.Parse(e.Attribute("id").Value);
                    sc.Description = e.Attribute("description").Value;
                    sc.MainCategroy = int.Parse(e.Attribute("main_categroy").Value);

                    mLst.Add(sc);
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
