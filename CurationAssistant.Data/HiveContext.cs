using CurationAssistant.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data
{
    public class HiveContext : DbContext
    {
        public HiveContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<FeedCache>()
                .HasKey(x => new { x.account_id, x.post_id });
            modelBuilder.Entity<Flag>()
                .HasKey(x => new { x.account, x.post_id });
            modelBuilder.Entity<Follow>()
                .HasKey(x => new { x.follower, x.following });
            modelBuilder.Entity<Member>()
                .HasKey(x => new { x.account, x.community });
            modelBuilder.Entity<PostTag>()
                .HasKey(x => new { x.post_id, x.tag });
            modelBuilder.Entity<ReBlog>()
                .HasKey(x => new { x.post_id, x.account });

            base.OnModelCreating(modelBuilder);            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<FeedCache> FeedCache { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ModLog> ModLogs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCache> PostCache { get; set; }
        public DbSet<ReBlog> ReBlogs { get; set; }
        public DbSet<State> States { get; set; }
    }
}
