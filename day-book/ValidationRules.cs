using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DayBook
{
    public class CategoryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "请选择分类!");
            }

            return new ValidationResult(true, null);
        }
    }

    public class MoneyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string input = value as string;
            if (input == "")
            {
                return new ValidationResult(false, "价格不能为空");
            }

            int money = 0;
            try
            {
                money = int.Parse(input);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "价格格式非法");
            }

            if (money <= 0 || money > short.MaxValue)
            {
                return new ValidationResult(false, string.Format("价格必须在 0 到 {0} 之间", short.MaxValue));
            }

            return new ValidationResult(true, null);
        }
    }
}
