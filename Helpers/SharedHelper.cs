using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Workdo.Models;

namespace Workdo.Helpers
{
    public class SharedHelper
    {

        public static bool IsEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            if (string.IsNullOrEmpty(value.Trim()))
                return true;
            return false;
        }

        /// <summary>
        /// Dữ liệu màu sắc
        /// </summary>
        public static List<string> ColorList()
        {
            return new List<string>
            {
            "#DC787E", "#F59E6C", "#986CF5", "#6CB4F5", "#F5D76C",
            "#485fc7", "#3e8ed0", "#48c78e", "#ffe08a", "#f14668",
            "#FF6633", "#FFB399", "#FF33FF", "#FFFF99", "#00B3E6",
            "#E6B333", "#3366E6", "#999966", "#99FF99", "#B34D4D",
            "#80B300", "#809900", "#E6B3B3", "#6680B3", "#66991A",
            "#FF99E6", "#CCFF1A", "#FF1A66", "#E6331A", "#33FFCC",
            "#66994D", "#B366CC", "#4D8000", "#B33300", "#CC80CC",
            "#66664D", "#991AFF", "#E666FF", "#4DB3FF", "#1AB399",
            "#E666B3", "#33991A", "#CC9999", "#B3B31A", "#00E680",
            "#4D8066", "#809980", "#E6FF80", "#1AFF33", "#999933",
            "#FF3380", "#CCCC00", "#66E64D", "#4D80CC", "#9900B3",
            "#E64D66", "#4DB380", "#FF4D4D", "#99E6E6", "#6666FF"
            };
        }


        /// <summary>
        /// Dữ liệu màu sắc
        /// </summary>
        public static string ColorRandom(int index)
        {
            var list = ColorList();
            if (index > 0)
            {
                if (index >= list.Count)
                    index = index % list.Count;
            }
            else
            {
                index = RandomInt(0, list.Count);
            }
            return list[index];
        }



        private static readonly Random random = new Random();

        /// <summary>
        /// Tạo một số ngẫu nhiên
        /// </summary>
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// Tính % tiến độ
        /// </summary>
        public static double Progress(double result, double target)
        {
            if (result > 0 && target > 0)
            {
                double progress = result * 100 / target;
                return progress > 100 ? 100 : progress;
            }
            else
                return 0;
        }

        /// <summary>
        /// Màu sắc của thanh phần trăm
        /// </summary>
        public static string ProgressColor(double progress)
        {
            string color = "is-dark";
            if (progress == 0)
                color = "is-dark";
            else if (progress < 25)
                color = "is-danger";
            else if (progress < 50)
                color = "is-warning";
            else if (progress < 75)
                color = "is-link";
            else if (progress >= 75)
                color = "is-success";

            return color;
        }


        /// <summary>
        /// Hàm tính phân trang
        /// </summary>
        public static int Paging(int total, int size)
        {
            if (total <= size)
                return 0;
            int col = total / size;
            float tp = total / (float)size;
            if (total % size != 0 && tp > col)
                col = total / size + 1;
            else
                col = total / size;
            return col;
        }


        /// <summary>
        /// Đổi cách hiển thị thời gian
        /// </summary>
        public static string ConvertDate(DateTime? date)
        {
            DateTime DateTimeNow = DateTime.Now;
            string postTime = string.Empty;
            if (date != null)
            {
                if (DateTime.Compare(date.Value, DateTimeNow) <= 0)
                {
                    TimeSpan spanMe = DateTimeNow.Subtract(date.Value);
                    if (spanMe.Days < 1)
                    {
                        if (spanMe.Hours < 1)
                        {
                            if (spanMe.Minutes < 1)
                            {
                                if (spanMe.Seconds < 5)
                                    postTime = "vừa xong";
                                else
                                    postTime = spanMe.Seconds + " giây trước";
                            }
                            else
                                postTime = spanMe.Minutes + " phút trước";
                        }
                        else
                            postTime = spanMe.Hours + " giờ trước";
                    }
                    else if (spanMe.Days < 30)
                    {
                        postTime = spanMe.Days + " ngày trước";
                    }
                    else if (spanMe.Days < 365)
                    {
                        postTime = Convert.ToInt32(spanMe.Days / 30) + " tháng trước";
                    }
                    else if (spanMe.Days > 365)
                    {
                        postTime = Convert.ToInt32(spanMe.Days / 365) + " năm trước";
                    }
                }
                else
                {
                    TimeSpan spanMe = date.Value.Subtract(DateTimeNow);
                    if (spanMe.Days < 1)
                    {
                        if (spanMe.Hours < 1)
                        {
                            if (spanMe.Minutes < 1)
                            {
                                if (spanMe.Seconds < 5)
                                    postTime = "bây giờ";
                                else
                                    postTime = spanMe.Seconds + " giây nữa";
                            }
                            else
                                postTime = spanMe.Minutes + " phút nữa";
                        }
                        else
                            postTime = spanMe.Hours + " giờ nữa";
                    }
                    else if (spanMe.Days < 30)
                    {
                        postTime = spanMe.Days + " ngày nữa";
                    }
                    else if (spanMe.Days < 365)
                    {
                        postTime = Convert.ToInt32(spanMe.Days / 30) + " tháng nữa";
                    }
                    else if (spanMe.Days > 365)
                    {
                        postTime = Convert.ToInt32(spanMe.Days / 365) + " năm nữa";
                    }
                }
            }

            return postTime;
        }



        /// <summary>
        /// Lấy khoản thời gian
        /// 1. Tuần này
        /// 2. Tháng này
        /// 3. Quý này
        /// 4. 30 ngày gần đây
        /// 5. 3 tháng gần đây
        /// 6. 6 tháng gần đây
        /// 11. Tuần trước
        /// 22. Tháng trước
        /// </summary>
        public static void GetTimeSpan(int type, out DateTime start, out DateTime end)
        {
            var date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            start = date;
            end = date;

            if (type == 1)
            {
                if (date.DayOfWeek == DayOfWeek.Tuesday)
                    start = date.AddDays(-1);
                else if (date.DayOfWeek == DayOfWeek.Wednesday)
                    start = date.AddDays(-2);
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                    start = date.AddDays(-3);
                else if (date.DayOfWeek == DayOfWeek.Friday)
                    start = date.AddDays(-4);
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                    start = date.AddDays(-5);
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                    start = date.AddDays(-6);
                end = start.AddDays(6);
            }
            else if (type == 11)
            {
                GetTimeSpan(1, out start, out end);
                start = start.AddDays(-7);
                end = end.AddDays(-7);
            }
            else if (type == 2)
            {
                start = Convert.ToDateTime(string.Format("{0:yyyy-MM-01}", DateTime.Now));
                end = start.AddMonths(1).AddDays(-1);
            }
            else if (type == 22)
            {
                start = Convert.ToDateTime(string.Format("{0:yyyy-MM-01}", DateTime.Now)).AddMonths(-1);
                end = start.AddMonths(1).AddDays(-1);
            }
            else if (type == 3)
            {
                if (date.Month <= 3)
                    start = Convert.ToDateTime(string.Format("{0:yyyy-01-01}", date));
                else if (date.Month <= 6)
                    start = Convert.ToDateTime(string.Format("{0:yyyy-04-01}", date));
                else if (date.Month <= 9)
                    start = Convert.ToDateTime(string.Format("{0:yyyy-07-01}", date));
                else
                    start = Convert.ToDateTime(string.Format("{0:yyyy-10-01}", date));
                end = start.AddMonths(3).AddDays(-1);
            }
            else if (type == 4)
            {
                start = date.AddDays(-30);
                end = date;
            }
            else if (type == 5)
            {
                start = Convert.ToDateTime(string.Format("{0:yyyy-MM-01}", DateTime.Now)).AddMonths(-2);
                end = start.AddMonths(3).AddDays(-1);
            }
            else if (type == 6)
            {
                start = Convert.ToDateTime(string.Format("{0:yyyy-MM-01}", DateTime.Now)).AddMonths(-5);
                end = start.AddMonths(6).AddDays(-1);
            }
        }






        public static bool DeviceMobile(string userAgent)
        {
            //Kiểm tra xem trình duyệt hiện tại là mobile hay ko?
            Regex mobile_b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex mobile_v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            bool isMobile = (mobile_b.IsMatch(userAgent) || mobile_v.IsMatch(userAgent.Substring(0, 4))) ? true : false;

            return isMobile;
        }

    }

}
