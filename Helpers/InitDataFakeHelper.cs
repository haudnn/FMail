using Workdo.Models;
using System.Collections.Generic;
using System;
using Faker.Extensions;
namespace Workdo.Helpers;
using System.Linq;
public class InitDataFakeHelper
{
    public static List<CategoryModel> InitCategories()
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        categories.Add(new CategoryModel() { id = "1", name = "Vận hành" });
        categories.Add(new CategoryModel() { id = "2", name = "Nhân sự" });
        categories.Add(new CategoryModel() { id = "3", name = "Kế toán" });
        categories.Add(new CategoryModel() { id = "4", name = "Sales marketing" });
        categories.Add(new CategoryModel() { id = "5", name = "Truyền thông" });
        categories.Add(new CategoryModel() { id = "6", name = "Tổng công ty" });
        return categories;
    }

    public static List<LabelModel> InitLabels()
    {
        List<LabelModel> labels = new List<LabelModel>();
        labels.Add(new LabelModel() { id = "1", name = "Nhân sự Conando", color = "#fff962" });
        labels.Add(new LabelModel() { id = "2", name = "Nhân sự Docorp", color = "#bcb51f" });
        labels.Add(new LabelModel() { id = "3", name = "Truyền thông 2023", color = "#ecf0ff" });
        labels.Add(new LabelModel() { id = "4", name = "Kế hoạch quý I", color = "#575e72" });
        labels.Add(new LabelModel() { id = "5", name = "Kế hoạch quý II", color = "#ffb4a9" });
        labels.Add(new LabelModel() { id = "6", name = "Plan tuần", color = "#5fcfe6" });
        return labels;
    }


    public static List<MailModel> InitMails()
    {
        List<MailModel> mails = new List<MailModel>();

        mails.Add(new MailModel()
        {
            id = "1",
            from = "Công ty TNHH Conando",
            subject = "Place.ai: Apply Now",
            body = "You wont regret it. I was amazed at the quality of it. I am really satisfied with my it.",
            isRead = true,
            isImportant = false,
            isDraft = false,
            isTrash = false,
            labels = new List<string>{"1","2","3", "4","5"},
            attachments = new List<AttachmentModel>
            {
                new AttachmentModel { name = "Filename.docx" },
                new AttachmentModel { name = "Filename.xlsx" },
                new AttachmentModel { name = "Filename.xlsx" },
                new AttachmentModel { name = "Filename.xlsx" },
                new AttachmentModel { name = "Filename.xlsx" },
            },
            sentDate = "8:14",
        });
        mails.Add(new MailModel()
        {
            id = "2",
            from = "Trang, tôi",
            subject = "Không có chủ đề",
            body = "You wont regret it. I was amazed at the quality of it. I am really satisfied with my it.",
            isRead = false,
            isImportant = true,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4", "3" },
            attachments = new List<AttachmentModel> { },
            sentDate = "7:25",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4"},
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
             mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4"},
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });

        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
        });
        mails.Add(new MailModel()
        {
            id = "3",
            from = "Lý Công Hoàng Anh",
            subject = "The results to our user testing",
            body = "",
            isRead = true,
            isImportant = false,
            isDraft = true,
            isTrash = false,
            labels = new List<string> { "4" },
            attachments = new List<AttachmentModel> { },
            sentDate = "30/12/2022",
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
        return groups;
    }


    public static List<MemberModel> InitMembers()
    {
        List<MemberModel> members = new List<MemberModel>();
        List<UserModel> users = InitUsers();
        foreach (UserModel user in users)
        {
            members.Add(new MemberModel
            {
                id = user.id,
                avatar = user.avatar,
                email = user.email,
                name = $"{user.first_name} {user.last_name}",
            });
        }
        return members;
    }


    

    public static List<MailModel> FilterMailByLabel(string labelId) 
    {
        List<MailModel> mails = InitMails();
        List<MailModel> filteredMails = mails.Where(mail => mail.labels.Contains(labelId)).ToList();
        if(filteredMails.Count == 0 ) {
            return new List<MailModel>();
        }
        return filteredMails;
    }



    public static List<UserModel> InitUsers() 
    {
        List<UserModel> users = new List<UserModel>();
        for (int i = 1; i < 21; i++) {
            users.Add(new UserModel
            {
                id = i.ToString(),
                email = "admin" + i.ToString() + "@gmail.com",
                password = "admin" + i.ToString(),
                first_name = "Nguyen",
                last_name = "Van" + " "+ i.ToString(),
                avatar = RandomAvatar(),
                session = "781e5e245d69b566979b86e28d23f2c7",
            });
        }
        return users;
    }


    public static string RandomAvatar() 
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 10001);
        return "https://avatars.dicebear.com/api/adventurer-neutral/" + randomNumber + ".svg";
    }


    public static List<MemberModel> GetMembersById(List<string> ids)
    {
        List<MemberModel> matchingMembers = new List<MemberModel>();
        List<MemberModel> members = InitMembers();
        foreach (string id in ids)
        {
            MemberModel member = members.Find(m => m.id == id);
            if (member != null)
            {
                matchingMembers.Add(member);
            }
        }
        return matchingMembers;
    }



    public static List<MemberModel> GetMembersUnion(List<MemberModel> members, List<MemberModel> membersDetail) 
    {
        var uniqueMembers = new List<MemberModel>();
        uniqueMembers = members.Union(membersDetail)
                           .GroupBy(m => m.id)
                           .Where(g => g.Count() == 1)
                           .Select(g => g.First())
                           .ToList();
        return uniqueMembers;
    }

}