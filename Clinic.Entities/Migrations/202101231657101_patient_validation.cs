namespace Clinic.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patient_validation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Patients", "Surname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Patients", "Adress", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Patients", "PostCode", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.Patients", "City", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "City", c => c.String(maxLength: 25));
            AlterColumn("dbo.Patients", "PostCode", c => c.String(maxLength: 6));
            AlterColumn("dbo.Patients", "Adress", c => c.String(maxLength: 50));
            AlterColumn("dbo.Patients", "Surname", c => c.String(maxLength: 20));
            AlterColumn("dbo.Patients", "Name", c => c.String(maxLength: 20));
        }
    }
}
