using System;

public class ObjectNames
{
    //Nombres para mostrar en el hidden object
    public static string GetObjectNameById(string id)
    {
        switch (id)
        {
            case "brujula":
                return "Brújula";
            case "botellas1":
                return "Botella azul";
            case "botellas2":
                return "Botella";
            case "botellas3":
                return "Botella marron";
            case "canon":
                return "Cañón";
            case "ancla":
                return "Ancla";
            case "hacha":
                return "Hacha";
            case "collar":
                return "Cadena de oro";
            case "monedaHechizada":
                return "Moneda de plata";
            case "vasija":
                return "Orinal";
            case "clavo":
                return "Clavo";
            case "lingoteOro":
                return "Lingote de oro";
            case "canilla":
                return "Canilla";
            case "pipa":
                return "Pipa";
            case "zapato":
                return "Zapato";
            case "estatua":
                return "Estatua";
            case "largavista":
                return "Largavista";
            case "balanza":
                return "Balanza";
            case "espada":
                return "Espada";
            case "balaCanon":
                return "Bala de cañón";
            case "monedaDeOro":
                return "Moneda de oro";
            case "monedaRustica":
                return "Moneda cuadrada";
            default:
                return id;
        }
    }

    //info para mostrar en hidden object al encontrar la moneda plateada, y en place object al colocar cada moneda
    public static ObjectFullData GetObjectFullDataById(string id)
    {
        switch (id)
        {
            case "monedaHechizada": //se usa en hidden object
                return new ObjectFullData(  "Moneda de plata", 
                                            "¡Encontraste la moneda de plata! ¡Qué suerte!", 
                                            "", 
                                            "InfoMonedas/foto_MonedaHechizada", 
                                            Links.InfoLinkMonedaPlata);
            case "mon_plata": //se usa en place object
                return new ObjectFullData(  "Moneda de plata Macuquina Plus Ultra de 8 reales", 
                                            "Soy fortuna y encanto en el mar, muchos barcos me quisieron llevar, pero al fondo de las aguas fueron a dar.", 
                                            "Soy fortuna y encanto en el mar, muchos barcos me quisieron llevar, pero al fondo de las aguas fueron a dar.", 
                                            "InfoMonedas/foto_MonedaHechizada",
                                            Links.InfoLinkMonedaPlata);
            case "mon_cuadrada": //se usa en place object
                return new ObjectFullData(  "Moneda recortada Macuquina de 8 reales", 
                                            "No soy redonda, más bien cuadrada, en los viejos tiempos fui muy preciada.", 
                                            "No soy redonda, más bien cuadrada, en los viejos tiempos fui muy preciada.", 
                                            "InfoMonedas/foto_MonedaCuadrada",
                                            Links.InfoLinkMonedaCuadrada);
            case "mon_corazon": //se usa en place object
                return new ObjectFullData(  "Moneda Macuquina de tipo transicional", 
                                            "Mi forma simboliza el amor, pocos conocen mi verdadero valor.", 
                                            "Mi forma simboliza el amor, pocos conocen mi verdadero valor.", 
                                            "InfoMonedas/foto_MonedaCorazon",
                                            Links.InfoLinkMonedaCorazon);
            case "mon_oro": //se usa en place object
                return new ObjectFullData(  "Moneda de oro “pelucona” de 8 escudos", 
                                            "Valiosa y dorada, por todos fuí deseada.", 
                                            "Valiosa y dorada, por todos fuí deseada.", 
                                            "InfoMonedas/foto_MonedaDorada",
                                            Links.InfoLinkMonedaOro);
            case "colgante": //se usa en place object
                return new ObjectFullData(  "Moneda hecha dije de collar", 
                                            "Joya brillante y  plateada, colgué de muchos cuellos en épocas pasadas.", 
                                            "Joya brillante y  plateada, colgué de muchos cuellos en épocas pasadas.", 
                                            "InfoMonedas/foto_MonedaDije",
                                            Links.InfoLinkMonedaDije);
            default:
                throw new Exception("No existe data para el item " + id);
        }
    }
}

public class ObjectFullData
{
    public ObjectFullData(string title, string description, string clue, string imgPath, string moreInfoLink)
    {
        Title = title;
        Description = description;
        Clue = clue;
        ImgPath = imgPath;
        MoreInfoLink = moreInfoLink;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Clue { get; private set; }
    public string ImgPath { get; private set; }
    public string MoreInfoLink { get; private set; }
}
