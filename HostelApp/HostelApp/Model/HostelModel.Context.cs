﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HostelApp.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HostelModelContainer : DbContext
    {
        public HostelModelContainer()
            : base("name=HostelModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> UserSet { get; set; }
        public virtual DbSet<Hostel> HostelSet { get; set; }
        public virtual DbSet<Room> RoomSet { get; set; }
        public virtual DbSet<Student> StudentSet { get; set; }
        public virtual DbSet<Person> PersonSet { get; set; }
        public virtual DbSet<Faculty> FacultySet { get; set; }
        public virtual DbSet<Group> GroupSet { get; set; }
        public virtual DbSet<Ocupation> OcupationSet { get; set; }
    }
}
