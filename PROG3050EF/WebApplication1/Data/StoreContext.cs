using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace GameStore.Data
{
    public partial class StoreContext : IdentityDbContext<User>
    {
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "1OBb$^#0^u21!"; // #ruzziaIsTerroristState 
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                user.EmailConfirmed = true;
                user.Email = "gamestore.prog3050@gmail.com";

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }


        public StoreContext()
        {

        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CreditCard> CreditCards { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<FriendsFamily> FriendsFamilies { get; set; } = null!;
        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<GameFeature> GameFeatures { get; set; } = null!;
        public virtual DbSet<GameImage> GameImages { get; set; } = null!;
        public virtual DbSet<Merchandise> Merchandises { get; set; } = null!;
        public virtual DbSet<MerchandiseImage> MerchandiseImages { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<ReviewImage> ReviewImages { get; set; } = null!;
        public virtual DbSet<WishList> WishLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DF-24;Initial Catalog=PROG_3050;Integrated Security=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CreditId)
                    .HasName("PK__CreditCa__C15A9C36A58DE279");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCreditCard326673");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PK__Customer__A1B71F90C6489007");

                entity.HasOne(d => d.PreferedCategory)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferedCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCustomer578024");

                entity.HasOne(d => d.PreferedPlatform)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferedPlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCustomer55737");

                entity.HasMany(d => d.Events)
                    .WithMany(p => p.Customers)
                    .UsingEntity<Dictionary<string, object>>(
                        "CustomerEvent",
                        l => l.HasOne<Event>().WithMany().HasForeignKey("Eventid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKCustomer_E485607"),
                        r => r.HasOne<Customer>().WithMany().HasForeignKey("Customerid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKCustomer_E75975"),
                        j =>
                        {
                            j.HasKey("Customerid", "Eventid").HasName("PK__Customer__333907D60F8269FF");

                            j.ToTable("Customer_Event");
                        });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__1299A861977CC9A6");
            });

            modelBuilder.Entity<FriendsFamily>(entity =>
            {
                entity.HasKey(e => e.FriendId)
                    .HasName("PK__FriendsF__3FA1E155F831F53C");

                entity.HasOne(d => d.CustId1Navigation)
                    .WithMany(p => p.FriendsFamilyCustId1Navigations)
                    .HasForeignKey(d => d.CustId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFriendsFam820474");

                entity.HasOne(d => d.CustId2Navigation)
                    .WithMany(p => p.FriendsFamilyCustId2Navigations)
                    .HasForeignKey(d => d.CustId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFriendsFam820475");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Games)
                    .UsingEntity<Dictionary<string, object>>(
                        "GameCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("Categoryid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGame_Categ786403"),
                        r => r.HasOne<Game>().WithMany().HasForeignKey("Gameid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGame_Categ26396"),
                        j =>
                        {
                            j.HasKey("Gameid", "Categoryid").HasName("PK__Game_Cat__0B57EBB7A12BD34A");

                            j.ToTable("Game_Category");
                        });
            });

            modelBuilder.Entity<GameFeature>(entity =>
            {
                entity.HasKey(e => e.FeatureId)
                    .HasName("PK__GameFeat__7906CBD7BF1F29A9");

                entity.HasMany(d => d.Games)
                    .WithMany(p => p.GameFeatures)
                    .UsingEntity<Dictionary<string, object>>(
                        "GameFeatureGame",
                        l => l.HasOne<Game>().WithMany().HasForeignKey("Gameid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGameFeatur387831"),
                        r => r.HasOne<GameFeature>().WithMany().HasForeignKey("GameFeatureid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGameFeatur415415"),
                        j =>
                        {
                            j.HasKey("GameFeatureid", "Gameid").HasName("PK__GameFeat__0E2BBB9711BBC777");

                            j.ToTable("GameFeature_Game");
                        });
            });

            modelBuilder.Entity<GameImage>(entity =>
            {
                entity.HasKey(e => e.GameImgId)
                    .HasName("PK__GameImag__132575EA33968AE7");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameImages)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGameImage381528");
            });

            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.HasKey(e => e.MerchId)
                    .HasName("PK__Merchand__1937E9C0E88FCC6F");
            });

            modelBuilder.Entity<MerchandiseImage>(entity =>
            {
                entity.HasKey(e => e.MerchImgId)
                    .HasName("PK__Merchand__4D99D94A9EAE7002");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.MerchandiseImages)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMerchandis483326");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder861201");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder547560");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder609133");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasMany(d => d.Games)
                    .WithMany(p => p.Platforms)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlatformGame",
                        l => l.HasOne<Game>().WithMany().HasForeignKey("Gameid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKPlatform_G103358"),
                        r => r.HasOne<Platform>().WithMany().HasForeignKey("Platformid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKPlatform_G477683"),
                        j =>
                        {
                            j.HasKey("Platformid", "Gameid").HasName("PK__Platform__B7F882CFAE338AEA");

                            j.ToTable("Platform_Game");
                        });
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReview172984");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReview486625");
            });

            modelBuilder.Entity<ReviewImage>(entity =>
            {
                entity.HasKey(e => e.ReviewImgId)
                    .HasName("PK__ReviewIm__ADCB53FF60CE66D7");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewImages)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReviewImag856029");
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasKey(e => e.WishId)
                    .HasName("PK__WishList__4F227CA0D99D4B73");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWishList705462");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWishList952486");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWishList795792");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
