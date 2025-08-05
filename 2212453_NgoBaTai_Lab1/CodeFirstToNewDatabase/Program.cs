using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CodeFirstToNewDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            using (var db = new BloggingContext())
            {
                //Create and save a new Blog

                // b. Thêm mới dữ liệu cho bảng Blog thông qua các câu lệnh: 
                //addBlog(db);

                //c. Thêm dữ liệu cho bảng Post 
                //addPost(db);

                //d. Display all Blogs from the database
                //displayBlogs(db);

                //e. Xoa post theo postID
                //deleteBlogId(db);

                //f. Update post
                //updatePost(db);

                //e. Xuat toan bo blog va post
                LogAll(db);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void addBlog(BloggingContext db)
        {
            //Thêm 1 chủ đề (Blog) vào csdl
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();
            var blog = new Blog { Name = name };
            db.Blogs.Add(blog);
            db.SaveChanges();
        }

        public static int? getBlogId(BloggingContext db, String name)
        {
            //Dùng LINQ syntax để truy vấn lấy blogID theo name
            int? blogId = db.Blogs
                .Where(b => b.Name == name)
                .Select(b => (int?)b.BlogId)
                .FirstOrDefault();

            return blogId;
        }

        public static void addPost(BloggingContext db)
        {
            Console.Write("Nhap tieu de: ");
            String? title = Console.ReadLine();

            Console.Write("Nhap noi dung: ");
            String? content = Console.ReadLine();

            Console.Write("Nhap ten Blog: ");
            String? name = Console.ReadLine();

            int? blogId = getBlogId(db, name);

            if (blogId.HasValue)
            {
                var post = new Post { Title = title, Content = content, BlogId = blogId };
                db.Posts.Add(post);
                db.SaveChanges();

                Console.WriteLine("Thêm thành công");
            }
            else
                Console.WriteLine("Không tìm thấy blog");


        }
        public static void displayBlogs(BloggingContext db)
        {
            Console.Write("Nhap ten Blog: ");
            String? name = Console.ReadLine();

            int? blogId = getBlogId(db, name);

            if (blogId.HasValue)
            {
                var queryRs = from p in db.Posts
                              where p.BlogId == blogId
                              select p;

                foreach(var post in queryRs)
                {
                    Console.WriteLine($"{post.PostId}. {post.Title} - {post.Content}");
                }
            }
        }

        public static void deleteBlogId(BloggingContext db)
        {   
            //Xoa blog
            var post = db.Posts.FirstOrDefault(p => p.PostId == 4);
            if (post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }

            //var post = new Post { PostId = 4 }; // chỉ cần set khóa chính
            //db.Posts.Attach(post); // attach vào context
            //db.Posts.Remove(post); // đánh dấu là Deleted
            //db.SaveChanges();
        }

        public static void updatePost(BloggingContext db)
        {
            //Cập nhật thông tin trong post
            var e = new Post { PostId = 3, Title = "Tin không tự nhiên"};
            db.Posts.Attach(e);
            db.Entry(e).Property(p => p.Title).IsModified = true;
            db.SaveChanges();

            Console.WriteLine("Cập nhật thành công");
        }

        public static void LogAll(BloggingContext db)
        {
            var blogs = db.Blogs
                .Include(b => b.Posts) 
                .ToList();
    
            foreach (var blog in blogs)
            {
                Console.WriteLine($"Blog: {blog.Name}");

                if (blog.Posts.Any())
                {
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($"   - {post.Title}: {post.Content}");
                    }
                }
                else
                {
                    Console.WriteLine("   (No posts)");
                }
            }
        }

}

}
