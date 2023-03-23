using Workdo.Models;
using System.Collections.Generic;
namespace Workdo.Helpers;
using System.Linq;
public class InitDataFakeHelper
{
    public static List<CategoryModel> InitCategories()
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        categories.Add(new CategoryModel() { id = 1, name = "Tất cả" });
        categories.Add(new CategoryModel() { id = 2, name = "Vận hành" });
        categories.Add(new CategoryModel() { id = 3, name = "Nhân sự" });
        categories.Add(new CategoryModel() { id = 4, name = "Kế toán" });
        categories.Add(new CategoryModel() { id = 6, name = "Sales marketing" });
        categories.Add(new CategoryModel() { id = 7, name = "Truyền thông" });
        categories.Add(new CategoryModel() { id = 8, name = "Tổng công ty" });
        return categories;
    }
    public static List<LabelModel> InitLabels()
    {
        List<LabelModel> labels = new List<LabelModel>();
        labels.Add(new LabelModel() { id = 1, name = "Nhân sự Conando", color = "#fff962" });
        labels.Add(new LabelModel() { id = 2, name = "Nhân sự Docorp", color = "#bcb51f" });
        labels.Add(new LabelModel() { id = 3, name = "Truyền thông 2023", color = "#ecf0ff" });
        labels.Add(new LabelModel() { id = 4, name = "Kế hoạch quý I", color = "#575e72" });
        labels.Add(new LabelModel() { id = 5, name = "Kế hoạch quý II", color = "#ffb4a9" });
        labels.Add(new LabelModel() { id = 6, name = "Plan tuần", color = "#5fcfe6" });
        return labels;
    }
    public static List<MailModel> InitMails()
    {
        List<MailModel> mails = new List<MailModel>();

        mails.Add(new MailModel()
        {
            id = 1,
            from = "Công ty TNHH Conando",
            subject = "Place.ai: Apply Now",
            body = "You wont regret it. I was amazed at the quality of it. I am really satisfied with my it.",
            isRead = true,
            isImportant = false,
            isDraft = false,
            isTrash = false,
            labels = new List<LabelModel>
            {
                new LabelModel { id = 3, name = "Kế hoạch quý II", color = "#ffb4a9" },
                new LabelModel { id = 4, name = "Plan tuần", color = "#5fcfe6" },
                new LabelModel { id = 1, name = "Truyền thông 2023", color = "#ecf0ff" },
                new LabelModel { id = 2, name = "Kế hoạch quý I", color = "#575e72" },
                new LabelModel { id = 5, name = "Plan tuần", color = "#5fcfe6" },
            },
            attachments = new List<AttachmentModel>
            {
                new AttachmentModel { name = "Filename.docx" },
                new AttachmentModel { name = "Filename.xlsx" },
                new AttachmentModel { name = "Filename.xlsx" },
                new AttachmentModel { name = "Filename.xlsx" },
                new AttachmentModel { name = "Filename.xlsx" },
            },
            receivedDate = "8:14",
        });
        mails.Add(new MailModel()
        {
            id = 2,
            from = "Trang, tôi",
            subject = "Không có chủ đề",
            body = "You wont regret it. I was amazed at the quality of it. I am really satisfied with my it.",
            isRead = false,
            isImportant = true,
            isDraft = true,
            isTrash = false,
            labels = new List<LabelModel>
            {
                new LabelModel { id = 4, name = "Plan tuần", color = "#5fcfe6" },
                new LabelModel { id = 3, name = "Kế hoạch quý II", color = "#ffb4a9" },
            },
            attachments = new List<AttachmentModel> { },
            receivedDate = "7:25",
        });
        mails.Add(new MailModel()
        {
            id = 3,
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<LabelModel>
            {
                new LabelModel { id = 4, name = "Plan tuần", color = "#5fcfe6" },
            },
            attachments = new List<AttachmentModel> { },
            receivedDate = "30/12/2022",
        });
        return mails;
    }
    public static List<MailModel> FitterMailsImportant()
    {
        List<MailModel> mails = InitMails();
        List<MailModel> mailsFilter = mails.Where(item => item.isImportant).ToList();
        return mailsFilter;
    }
    public static List<MailModel> FitterMailsDraft()
    {
        List<MailModel> mails = InitMails();
        List<MailModel> mailsFilter = mails.Where(item => item.isDraft).ToList();
        return mailsFilter;
    }
    public static List<MailModel> FitterMailsNotDraft()
    {
        List<MailModel> mails = InitMails();
        List<MailModel> mailsFilter = mails.Where(item => !item.isDraft).ToList();
        return mailsFilter;
    }
    public static List<GroupModel> InitGroups()
    {
        List<GroupModel> groups = new List<GroupModel>();
        groups.Add(new GroupModel()
        {
            id = "1",
            name = "HEADBOM",
            members = new List<MemberModel>
            {
                new MemberModel
                {
                    id = "1",
                    name = "Đinh Thùy Linh",
                    avatar = "https://images.pexels.com/photos/10452216/pexels-photo-10452216.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                    email = "linhdt.conando@gmail.com"
                },
                new MemberModel
                {
                    id = "2",
                    name = "Khánh Hoàn",
                    avatar = "https://tiemchupanh.com/wp-content/uploads/2021/11/5-6.jpg",
                    email = "kh.conando@gmail.com"
                },
                new MemberModel {
                    id = "3",
                    name = "Nguyễn Lê Hải Phong",
                    avatar = "https://phunugioi.com/wp-content/uploads/2022/02/anh-chan-dung-nu-duom-buon.jpg",
                    email = "nlhp.conando@gmail.com"
                }
            },
        });
        groups.Add(new GroupModel()
        {
            id = "2",
            name = "SBOM",
            members = new List<MemberModel>
            {
                new MemberModel
                {
                    id = "1",
                    name = "Đinh Thùy Linh",
                    avatar = "https://images.pexels.com/photos/10452216/pexels-photo-10452216.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                    email = "linhdt.conando@gmail.com"
                },
                new MemberModel
                {
                    id = "4",
                    name = "Hà Nguyễn",
                    avatar = "https://hoangphucphoto.com/wp-content/uploads/2022/12/aenh-chan-dung.jpg",
                    email = "hn.conando@gmail.com"
                }
            },
        });
        groups.Add(new GroupModel()
        {
            id = "3",
            name = "Vận hành",
            members = new List<MemberModel>
            {
                new MemberModel
                {
                    id = "1",
                    name = "Đinh Thùy Linh",
                    avatar = "https://images.pexels.com/photos/10452216/pexels-photo-10452216.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                    email = "linhdt.conando@gmail.com"
                },
                new MemberModel
                {
                    id = "2",
                    name = "Khánh Hoàn",
                    avatar = "https://tiemchupanh.com/wp-content/uploads/2021/11/5-6.jpg",
                    email = "kh.conando@gmail.com"
                }
            },
        });
        groups.Add(new GroupModel()
        {
            id = "4",
            name = "Hangout",
            members = new List<MemberModel>
            {
                new MemberModel
                {
                    id = "1",
                    name = "Đinh Thùy Linh",
                    avatar = "https://images.pexels.com/photos/10452216/pexels-photo-10452216.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                    email = "linhdt.conando@gmail.com"
                },
                new MemberModel
                {
                    id = "5",
                    name = "Trần Minh Đức",
                    avatar = "https://thuthuatnhanh.com/wp-content/uploads/2021/11/Hinh-anh-chan-dung-em-be.jpg",
                    email = "tmd.conando@gmail.com"
                }
            },
        });

        return groups;
    }
    public static List<MemberModel> InitMembers()
    {
        List<MemberModel> members = new List<MemberModel>();
        members.Add(new MemberModel()
        {
            id = "1",
            name = "Đinh Thùy Linh",
            avatar = "https://images.pexels.com/photos/10452216/pexels-photo-10452216.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
            email = "linhdt.conando@gmail.com"
        });
        members.Add(new MemberModel()
        {
            id = "2",
            name = "Khánh Hoàn",
            avatar = "https://tiemchupanh.com/wp-content/uploads/2021/11/5-6.jpg",
            email = "kh.conando@gmail.com"
        });
        members.Add(new MemberModel()
        {
            id = "3",
            name = "Nguyễn Lê Hải Phong",
            avatar = "https://phunugioi.com/wp-content/uploads/2022/02/anh-chan-dung-nu-duom-buon.jpg",
            email = "nlhp.conando@gmail.com"
        });
        members.Add(new MemberModel()
        {
            id = "4",
            name = "Hà Nguyễn",
            avatar = "https://hoangphucphoto.com/wp-content/uploads/2022/12/aenh-chan-dung.jpg",
            email = "hn.conando@gmail.com"
        });
        members.Add(new MemberModel()
        {
            id = "5",
            name = "Trần Minh Đức",
            avatar = "https://thuthuatnhanh.com/wp-content/uploads/2021/11/Hinh-anh-chan-dung-em-be.jpg",
            email = "tmd.conando@gmail.com"
        });
        return members;
    }
}