namespace PTurismo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ficheiros : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        nome = c.String(unicode: false),
                        genero = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Poi",
                c => new
                    {
                        PoiID = c.Int(nullable: false, identity: true),
                        CategoriaID = c.Int(nullable: false),
                        nome = c.String(unicode: false),
                        latitude = c.Double(nullable: false),
                        longitude = c.Double(nullable: false),
                        descricao = c.String(unicode: false),
                        imagem = c.String(unicode: false),
                        resumo = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.PoiID)
                .ForeignKey("dbo.Categoria", t => t.CategoriaID, cascadeDelete: true)
                .Index(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Elemento",
                c => new
                    {
                        ElementoID = c.Int(nullable: false, identity: true),
                        PoiID = c.Int(nullable: false),
                        nome = c.String(unicode: false),
                        descricao = c.String(unicode: false),
                        imagem = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ElementoID)
                .ForeignKey("dbo.Poi", t => t.PoiID, cascadeDelete: true)
                .Index(t => t.PoiID);
            
            CreateTable(
                "dbo.GaleriaElemento",
                c => new
                    {
                        GaleriaElementoID = c.Int(nullable: false, identity: true),
                        ElementoID = c.Int(nullable: false),
                        media = c.String(unicode: false),
                        legenda = c.String(unicode: false),
                        tipoMedia = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.GaleriaElementoID)
                .ForeignKey("dbo.Elemento", t => t.ElementoID, cascadeDelete: true)
                .Index(t => t.ElementoID);
            
            CreateTable(
                "dbo.GaleriaPoi",
                c => new
                    {
                        GaleriaPoiID = c.Int(nullable: false, identity: true),
                        Legenda = c.String(unicode: false),
                        PoiID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GaleriaPoiID)
                .ForeignKey("dbo.Poi", t => t.PoiID, cascadeDelete: true)
                .Index(t => t.PoiID);
            
            CreateTable(
                "dbo.FilePathPoi",
                c => new
                    {
                        FilePathPoiID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Descricao = c.String(unicode: false),
                        FileType = c.Int(nullable: false),
                        GaleriaPoiID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathPoiID)
                .ForeignKey("dbo.GaleriaPoi", t => t.GaleriaPoiID, cascadeDelete: true)
                .Index(t => t.GaleriaPoiID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GaleriaPoi", "PoiID", "dbo.Poi");
            DropForeignKey("dbo.FilePathPoi", "GaleriaPoiID", "dbo.GaleriaPoi");
            DropForeignKey("dbo.Elemento", "PoiID", "dbo.Poi");
            DropForeignKey("dbo.GaleriaElemento", "ElementoID", "dbo.Elemento");
            DropForeignKey("dbo.Poi", "CategoriaID", "dbo.Categoria");
            DropIndex("dbo.FilePathPoi", new[] { "GaleriaPoiID" });
            DropIndex("dbo.GaleriaPoi", new[] { "PoiID" });
            DropIndex("dbo.GaleriaElemento", new[] { "ElementoID" });
            DropIndex("dbo.Elemento", new[] { "PoiID" });
            DropIndex("dbo.Poi", new[] { "CategoriaID" });
            DropTable("dbo.FilePathPoi");
            DropTable("dbo.GaleriaPoi");
            DropTable("dbo.GaleriaElemento");
            DropTable("dbo.Elemento");
            DropTable("dbo.Poi");
            DropTable("dbo.Categoria");
        }
    }
}
