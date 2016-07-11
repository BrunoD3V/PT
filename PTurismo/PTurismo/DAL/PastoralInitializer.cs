using PTurismo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PTurismo.DAL
{
    public class PastoralInitializer : CreateDatabaseIfNotExists<PastoralContext>
    {
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
                new Poi { PoiID=0, nome="Igreja Matriz Vale de Salgueiro", CategoriaID = 1, latitude=41.590878, longitude=-7.235249,
                    descricao ="Igreja descrita",
                    resumo = "Igreja resumida"},

                new Poi { PoiID=1, nome="Sé Velha", CategoriaID = 2, latitude=41.805923, longitude=-6.756663,
                    descricao ="Sé Velha descrita",
                    resumo = "Sé Velha resumida"},

                new Poi { PoiID=2, nome="Sé de Miranda", CategoriaID = 2, latitude=41.492987, longitude=-6.273409,
                    descricao ="Catedral Miranda descrição",
                    resumo = "Catedral de Miranda resumida"},

                new Poi { PoiID=3, nome="Nossa Senhora do Amparo", CategoriaID = 5, latitude=41.483889, longitude=-7.187343,
                    descricao ="Nossa Senhora do Amparo descrição",
                    resumo = "Nossa Senhora do Amparo resumida"},

                new Poi { PoiID=4, nome="Igreja de Vinhais", CategoriaID = 1, latitude=41.832420, longitude=-7.007088,
                    descricao ="Igreja de Vinhais descrita",
                    resumo = "Igreja de Vinhais resumida"},

                new Poi { PoiID=5, nome="Museu Municipal Armindo Teixeira Lopes", CategoriaID = 7, latitude=41.485280, longitude=-7.178779,
                    descricao ="Museu Municipal Armindo Teixeira Lopes descrita",
                    resumo = "Museu Municipal Armindo Teixeira Lopes resumida"},

                new Poi { PoiID=6, nome="Basílica Menor do Santo Cristo de Outeiro", CategoriaID = 6, latitude=41.683693, longitude=-6.600570,
                    descricao ="Basilica Menor do Santo Cristo de Outeiro descrita",
                    resumo = "Basilica Menor do Santo Cristo de Outeiro resumida"},

                new Poi { PoiID=7, nome="Convento de Balsemão", CategoriaID = 4, latitude=41.474379, longitude=-6.855900,
                    descricao ="Convento de Balsemão descrita",
                    resumo = "Convento de Balsemão resumida"},

                new Poi { PoiID=8, nome="Capela de Santo Amaro", CategoriaID = 3, latitude=41.645415, longitude=-6.600713,
                    descricao ="Capela de Santo Amaro descrita",
                    resumo = "Capela de Santo Amaro resumida"},
                new Poi { PoiID=9, nome="Igreja de São Miguel Arcanjo", CategoriaID = 1, latitude=41.4124905, longitude=-7.1734237, descricao="Arquitectura religiosa, barroca e rococó. Fachada principal em empena recortada, com portal em arco abatido, sobrepujado por nicho concheado ladeado por dois óculos elípticos.Retábulos laterais rococó, sendo os colaterais, em ângulo, e o mor revivalistas neobarrocos.", resumo="" },
                new Poi { PoiID=10, nome="Igreja de Santo Estevão", CategoriaID=1, latitude=41.8654563, longitude=-6.9876812, descricao="Igreja maneirista de planta rectangular, com um alpendre quadrangular. Tem uma nave única, capela-mor e sacristia adossada do lado esquerdo. Apresenta cobertura única em telhados de duas águas, excepto no alpendre onde a cobertura é de três águas. Todos os alçados são percorridos por um embasamento e cornija, com cunhais rematados por pináculos.",  resumo="" },
                new Poi { PoiID=11, nome="Santuário da Senhora da Serra", CategoriaID=5, latitude=41.5001888, longitude=-7.5123275, descricao="o Santuário da Senhora da Serra, também conhecido como o Santuário de Nossa Senhora das Neves, é uma igreja de três naves, separadas por colunas e pilastras cujos capitéis, de aspecto arcaico, parecem ser do século XV ou XVI.O altar-mor, de talha setecentista, foi restaurado nos anos 70 e a imagem da Senhora da Serra é do início do século XX.",  resumo="" },
                new Poi { PoiID=12, nome="Igreja de São Pedro", CategoriaID=1, latitude=41.8036206,longitude=-6.7509715,descricao="A igreja da Moimenta, vista exteriormente, impõe-se sobremaneira, em todo o conjunto arquitetónico. A fachada principal, de linhas simples, contrasta com a magnificência das torres quadradas de estilo românico e a elegância artística da balaustrada de granito entre os flocos das suas bases retas. Deve ser um dos templos maiores da região, provavelmente a maior igreja Paroquial de terras de Vinhais.",  resumo=""},
                new Poi { PoiID=13, nome="Capela De Nossa Senhora do Areal", CategoriaID=3, latitude=41.6954632, longitude=-7.0612736, descricao="Capela de planta longitudinal, com alpendre no frontispício. Na nave, do lado do Evangelho, numa edícula há uma representação escultórica de São Pedro. Na parte de trás do altar - mor, de talha e da pintura, subsistem frescos numa área de 3,80m X 2m.", resumo="" },
                new Poi { PoiID=14, nome="Capela de São Bartolomeu", CategoriaID=3, latitude=41.6620952,longitude=-6.9084036, descricao="no recinto da capela há um cruzeiro, colocado em frente à sua entrada. A ermida de linhas modernas, tem adossada do lado direito uma torre sineira de três registos, no dos quais se rasga um óculo e no terceiro surgem os sinos.",  resumo=""},
                new Poi { PoiID=15, nome="Santuário de Nossa Senhora do Aviso", CategoriaID=5, latitude=41.599406, longitude=-6.765357, descricao="Neste local além do santuário, composto por oito templetes alusivos ao calvário, fica também a fonte dos Engaranhos com umas inscrições imperceptíveis.", resumo="" },
                new Poi { PoiID=16, nome="Igreja Matriz de Santa Comba da Vilariça", CategoriaID=1, latitude=41.1533885, longitude=-7.0148377, descricao=" Igreja barroca que guarda no seu interior dois altares de talha dourada e, no lado do Evangelho, um púlpito quadrado com mísula pétrea. Ostenta um tecto de madeira formado por grandes painéis.",  resumo="" },
                new Poi { PoiID=17, nome="Igreja de Vale de Prados", CategoriaID=1, latitude=41.6178244,longitude=-7.2266654, descricao="Vale de Prados de Ledra chamava se assim porque existia na Terra de Ledra e para distinguir de Vale de Prados de Macedo de Cavaleiros.O Orago era Santo André.A paróquia foi instituída pela abadia de Guide depois do século XV na ermida local dedicada ao apóstolo.Foi unida a Múrias depois da extinção da paróquia(cerca de 1834).",  resumo="" },
                new Poi { PoiID=18, nome="Santuário de N. Sra. da Assunção", CategoriaID=5, latitude=41.2837901, longitude=-7.4999258, descricao="É o maior e um dos mais importantes santuários Marianos de Trás-os-Montes. Erguido no século XIX no alto de um monte que domina toda a paisagem envolvente, representa um dos pontos mais altos do Concelho, com cerca de 760 metros de altitude.O Santuário de N. Sra.da Assunção, é também Miradouro de primeira qualidade.Junto ao varandim do adro obtém-se uma rara e vasta paisagem, cobrindo a vizinha Sanábria, Montesinho, Bornes, Mirandela e as vilas e aldeias vizinhas num raio de 100km.A sua história é milenar visto ter existido um castro neste magnífico monte, que justamente foi escolhido pela sua capacidade de Posto de Vigia.Possui uma igreja de nave única e capela-mor retangulares, várias capelinhas espalhadas pelo recinto e um monumental escadório, tudo envolto em imensos tufos de floresta.Santuário de N.Sra. do Rosário, em Freixiel.",  resumo="" }

            };

            pois.ForEach(p => Context.Poi.Add(p));
            Context.SaveChanges();

            var elementos = new List<Elemento>
            {
                new Elemento { ElementoID=1, nome="Nave central da igreja", descricao="Entrada da Igreja",
                   PoiID=2},
                new Elemento { ElementoID=2, nome="Capela Mor", descricao="Capela Mor descrição",
                     PoiID=2},
                new Elemento { ElementoID=3, nome="Portal de São Gonçalo de Amarante", descricao="São Gonçalo Amarante",
                     PoiID=2}
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
                new GaleriaPoi {GaleriaPoiID=1, Legenda="Video do Exterior da Catedral",  PoiID=2 },
                new GaleriaPoi {GaleriaPoiID=2, Legenda="Video do Interior da Catedral",  PoiID=2 }
            };

            galeriaPois.ForEach(p => Context.GaleriaPoi.Add(p));
            Context.SaveChanges();

        }
    }
}