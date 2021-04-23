namespace Clinic.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patient_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "AccountId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "AccountId");
        }
    }
}
