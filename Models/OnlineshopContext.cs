using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DabaBase.Models;

public partial class OnlineshopContext : DbContext
{
    public OnlineshopContext()
    {
    }

    public OnlineshopContext(DbContextOptions<OnlineshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<OrderDtl> OrderDtls { get; set; }

    public virtual DbSet<OrderTbl> OrderTbls { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("Brand_id");
            entity.Property(e => e.BrandDesc)
                .HasMaxLength(50)
                .HasColumnName("Brand_Desc");
            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .HasColumnName("Brand_Name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityId).HasColumnName("City_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .HasColumnName("City__Name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("Comment_id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.ReplyingId).HasColumnName("Replying_id");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.Customer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Comment_Customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Comment_Products");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.CityId).HasColumnName("City_id");
            entity.Property(e => e.CustomerFName)
                .HasMaxLength(50)
                .HasColumnName("Customer_fName");
            entity.Property(e => e.CustomerLName)
                .HasMaxLength(50)
                .HasColumnName("Customer_lName");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Postalcode)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.City).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Customer_City");
        });

        modelBuilder.Entity<OrderDtl>(entity =>
        {
            entity.HasKey(e => e.OrderdetailId);

            entity.ToTable("Order_dtl");

            entity.Property(e => e.OrderdetailId).HasColumnName("Orderdetail_id");
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDtls)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Order_dtl_Order_tbl");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDtls)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Order_dtl_Products");
        });

        modelBuilder.Entity<OrderTbl>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("Order_tbl");

            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.IsDelivered)
                .HasMaxLength(50)
                .HasColumnName("is_Delivered");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderTbls)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_tbl_Customer");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.BrandId).HasColumnName("Brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.PchangeDate).HasColumnType("datetime");
            entity.Property(e => e.ProductDesc)
                .HasMaxLength(50)
                .HasColumnName("Product_Desc");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("Product_Name");
            entity.Property(e => e.ProductPrice).HasColumnName("Product_Price");
            entity.Property(e => e.ProductStock).HasColumnName("Product_Stock");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Products_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
