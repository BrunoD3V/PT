namespace PTurismo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alskd : DbMigration
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
                        FilePathCategoria_FilePathCategoriaID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoriaID)
                .ForeignKey("dbo.FilePathCategoria", t => t.FilePathCategoria_FilePathCategoriaID)
                .Index(t => t.FilePathCategoria_FilePathCategoriaID);
            
            CreateTable(
                "dbo.FilePathCategoria",
                c => new
                    {
                        FilePathCategoriaID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Descricao = c.String(unicode: false),
                        FileType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathCategoriaID);
            
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
                        ImagemPath = c.String(unicode: false),
                        FileType = c.Int(nullable: false),
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
                        ImagemElemento_FilePathElementoID = c.Int(),
                    })
                .PrimaryKey(t => t.ElementoID)
                .ForeignKey("dbo.FilePathElemento", t => t.ImagemElemento_FilePathElementoID)
                .ForeignKey("dbo.Poi", t => t.PoiID, cascadeDelete: true)
                .Index(t => t.PoiID)
                .Index(t => t.ImagemElemento_FilePathElementoID);
            
            CreateTable(
                "dbo.GaleriaElemento",
                c => new
                    {
                        GaleriaElementoID = c.Int(nullable: false, identity: true),
                        ElementoID = c.Int(nullable: false),
                        legenda = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.GaleriaElementoID)
                .ForeignKey("dbo.Elemento", t => t.ElementoID, cascadeDelete: true)
                .Index(t => t.ElementoID);
            
            CreateTable(
                "dbo.FilePathElemento",
                c => new
                    {
                        FilePathElementoID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255, storeType: "nvarchar"),
                        FileType = c.Int(nullable: false),
                        GaleriaElementoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathElementoID)
                .ForeignKey("dbo.GaleriaElemento", t => t.GaleriaElementoID, cascadeDelete: true)
                .Index(t => t.GaleriaElementoID);
            
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
            DropForeignKey("dbo.Elemento", "ImagemElemento_FilePathElementoID", "dbo.FilePathElemento");
            DropForeignKey("dbo.FilePathElemento", "GaleriaElementoID", "dbo.GaleriaElemento");
            DropForeignKey("dbo.GaleriaElemento", "ElementoID", "dbo.Elemento");
            DropForeignKey("dbo.Poi", "CategoriaID", "dbo.Categoria");
            DropForeignKey("dbo.Categoria", "FilePathCategoria_FilePathCategoriaID", "dbo.FilePathCategoria");
            DropIndex("dbo.FilePathPoi", new[] { "GaleriaPoiID" });
            DropIndex("dbo.GaleriaPoi", new[] { "PoiID" });
            DropIndex("dbo.FilePathElemento", new[] { "GaleriaElementoID" });
            DropIndex("dbo.GaleriaElemento", new[] { "ElementoID" });
            DropIndex("dbo.Elemento", new[] { "ImagemElemento_FilePathElementoID" });
            DropIndex("dbo.Elemento", new[] { "PoiID" });
            DropIndex("dbo.Poi", new[] { "CategoriaID" });
            DropIndex("dbo.Categoria", new[] { "FilePathCategoria_FilePathCategoriaID" });
            DropTable("dbo.FilePathPoi");
            DropTable("dbo.GaleriaPoi");
            DropTable("dbo.FilePathElemento");
            DropTable("dbo.GaleriaElemento");
            DropTable("dbo.Elemento");
            DropTable("dbo.Poi");
            DropTable("dbo.FilePathCategoria");
            DropTable("dbo.Categoria");
        }
    }
}
