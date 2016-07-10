function markerClick(args) {

    if (args.eventName === 'click') {
        var eles = new Array();
        try {
            var ind = parseInt(args.id) + 1;

            for (var i = 0; i < elementos.length; i++) {
                var obj = elementos[i];
                for (var key in obj) {
                    var attrName = key;
                    var attrValue = obj[key];

                    if (attrName === "PoiID" && attrValue == args.id) {

                        eles.push(elementos[i]);
                        console.log(eles[i]);
                    }
                }
            }

        } catch (err) {
            //$('#Elemento').html('<div></div>');
        }

        var catID = pois[args.id].CategoriaID;

        $('#NomePoi').html('<div> nome:' + pois[args.id].nome + '</div>');
        $('#Descricao').html(pois[args.id].descricao);
        $('#Categoria').html(categorias[catID - 1].nome);
        if (eles.length > 1) {

            for (var j = 0; j < eles.length; j++) {
                console.log(j);
                //TODO mostrar cada elemento do poi
            }
        }


        //alert(pois[args.id].nome);
    }

}
