namespace PTurismo.Migrations
{
    using DAL;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PTurismo.DAL.PastoralContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(PastoralContext Context)
        {
            var categorias = new List<Categoria>
            {
                new Categoria {CategoriaID=1,nome="Igreja", genero="Religioso"},
                new Categoria {CategoriaID=2,nome="Catedral", genero="Religioso"},
                new Categoria {CategoriaID=3,nome="Capela", genero="Religioso"},
                new Categoria {CategoriaID=4,nome="Mosteiro", genero="Religioso"},
                new Categoria {CategoriaID=5,nome="Santuário", genero="Religioso"},
                new Categoria {CategoriaID=6,nome="Basílica", genero="Religioso"},
                new Categoria {CategoriaID=7,nome="Museu", genero="Civil"},
                new Categoria {CategoriaID=8,nome="Castelo", genero="Militar"}
            };

            categorias.ForEach(c => Context.Categoria.Add(c));
            Context.SaveChanges();

            var pois = new List<Poi>
            {
                new Poi { PoiID=1, nome="Sé Velha", CategoriaID = 2, latitude=41.805923, longitude=-6.756663,
                    descricao ="Sé Velha descrita", imagem="https://pt.wikipedia.org/wiki/S%C3%A9_Velha_de_Bragan%C3%A7a#/media/File:Bragan%C3%A7a_Old_Cathedral.jpg",
                    resumo = "Sé Velha resumida"},

                new Poi { PoiID=2, nome="Sé de Miranda", CategoriaID = 2, latitude=41.492987, longitude=-6.273409,
                    descricao ="Catedral Miranda descrição", imagem="https://pt.wikipedia.org/wiki/Igreja_de_Miranda_do_Douro#/media/File:Igreja_Matriz_de_Miranda_do_Douro.jpg",
                    resumo = "Catedral de Miranda resumida"},

                new Poi { PoiID=3, nome="Nossa Senhora do Amparo", CategoriaID = 5, latitude=41.483889, longitude=-7.187343,
                    descricao ="Nossa Senhora do Amparo descrição", imagem="http://cdn1.igogo.pt/fotos/11/26/capela-de-nossa-senhora-do-amparo-24-2.jpg",
                    resumo = "Nossa Senhora do Amparo resumida"},

                new Poi { PoiID=4, nome="Igreja de Vinhais", CategoriaID = 1, latitude=41.832420, longitude=-7.007088,
                    descricao ="Igreja de Vinhais descrita", imagem="http://www.turismovinhais.com/wp-content/uploads/figueira-igreja-sao-francisco-500x295.jpg",
                    resumo = "Igreja de Vinhais resumida"},

                new Poi { PoiID=5, nome="Museu Municipal Armindo Teixeira Lopes", CategoriaID = 7, latitude=41.485280, longitude=-7.178779,
                    descricao ="Museu Municipal Armindo Teixeira Lopes descrita", imagem="http://static.panoramio.com/photos/original/71138174.jpg",
                    resumo = "Museu Municipal Armindo Teixeira Lopes resumida"},

                new Poi { PoiID=6, nome="Basílica Menor do Santo Cristo de Outeiro", CategoriaID = 6, latitude=41.683693, longitude=-6.600570,
                    descricao ="Basilica Menor do Santo Cristo de Outeiro descrita", imagem="http://www.rotaterrafria.com/uploads/geo_article/image/4762/igreja_santo_cristo1_outeiro.jpg",
                    resumo = "Basilica Menor do Santo Cristo de Outeiro resumida"},

                new Poi { PoiID=7, nome="Convento de Balsemão", CategoriaID = 4, latitude=41.474379, longitude=-6.855900,
                    descricao ="Convento de Balsemão descrita", imagem="http://q-ec.bstatic.com/images/hotel/max400/634/63400060.jpg",
                    resumo = "Convento de Balsemão resumida"},

                new Poi { PoiID=8, nome="Capela de Santo Amaro", CategoriaID = 3, latitude=41.645415, longitude=-6.600713,
                    descricao ="Capela de Santo Amaro descrita", imagem="http://cdn.igogo.pt/fotos/10/03/capela-de-santo-amaro-152-1.jpg",
                    resumo = "Capela de Santo Amaro resumida"}
            };

            pois.ForEach(p => Context.Poi.Add(p));
            Context.SaveChanges();

            var elementos = new List<Elemento>
            {
                new Elemento { ElementoID=1, nome="Nave central da igreja", descricao="Entrada da Igreja",
                    imagem ="http://www.culturanorte.pt/fotos/galerias/miranda_do_douro_5_170883449654e2220c04ed4.jpg", PoiID=2},
                new Elemento { ElementoID=2, nome="Capela Mor", descricao="Capela Mor descrição",
                    imagem ="http://www.culturanorte.pt/fotos/galerias/miranda_do_douro_3_153605294954e203b0c93d9.jpg", PoiID=2},
                new Elemento { ElementoID=3, nome="Portal de São Gonçalo de Amarante", descricao="São Gonçalo Amarante",
                    imagem ="https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Ig_s_gocalo_amarante_porta_03.JPG/286px-Ig_s_gocalo_amarante_porta_03.JPG", PoiID=2}
            };

            elementos.ForEach(e => Context.Elemento.Add(e));
            Context.SaveChanges();

            var galeriaElementos = new List<GaleriaElemento>
            {
                new GaleriaElemento { GaleriaElementoID=1, media="Navecentral.mpeg", legenda="Video Nave Central", tipoMedia="Video", ElementoID=1 },
                new GaleriaElemento { GaleriaElementoID=2, media="http://www.patrimoniocultural.pt/static/data/cache/d5/c4/d5c439d97808ff47aa72849fcd828d4c.jpg",
                    legenda ="Fotografia do Teto da Nave Central", tipoMedia="Fotografia", ElementoID=1 },
                new GaleriaElemento { GaleriaElementoID=2, media="CapelaMor.mpeg",legenda ="Video Capela Mor", tipoMedia="Video", ElementoID=2 }
            };

            galeriaElementos.ForEach(g => Context.GaleriaElemento.Add(g));
            Context.SaveChanges();

            var galeriaPois = new List<GaleriaPoi>
            {
                new GaleriaPoi {GaleriaPoiID=1, media="CatedralMiranda.mpeg", legenda="Video do Exterior da Catedral", tipoMedia="Video", PoiID=2 },
                new GaleriaPoi {GaleriaPoiID=2, media="CatedralMirandaInterior.mpeg", legenda="Video do Interior da Catedral", tipoMedia="Video", PoiID=2 }
            };

            galeriaPois.ForEach(p => Context.GaleriaPoi.Add(p));
            Context.SaveChanges();
        }
    }
}
