namespace Clinic.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patient_validationFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Phone", c => c.Int(nullable: false));
        }
    }
}
