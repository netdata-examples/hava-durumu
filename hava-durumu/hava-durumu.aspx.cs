using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using System.Data;
using System.Xml;
using System.Text;
using System.Web.Services;

public partial class hava_durumu : System.Web.UI.Page
{
    protected string MeteorolojiXmlLink = "https://www.netdata.com/XML/8f978530";
    protected string YandexXmlLink = "https://www.netdata.com/XML/763e77a2";
    protected string YahooXmlLink = "https://www.netdata.com/XML/87a40abe";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SehirleriYukle();

            IList<string> segmentler = Request.GetFriendlyUrlSegments();
            if (segmentler.Count == 2)
            {
                string kategori = segmentler[1].ToString();
                string sehirSeo = segmentler[0].ToString().Replace("-hava-durumu", "");

                VerileriYukle(sehirSeo, kategori);
            }
            else if (segmentler.Count == 1)
            {
                string kategori = "";
                string sehirSeo = segmentler[0].ToString().Replace("-hava-durumu", "");

                VerileriYukle(sehirSeo, kategori);
            }
            else
            {
                VerileriYukle("istanbul", "meteoroloji");
            }
        }
        catch (Exception)
        {
        }
    }

    public void VerileriYukle(string sehirSeo, string kategori)
    {
        string url = "";
        string descriptionText = "";
        string keywordsText = "";
        MeteorolojiPanel.Visible = false;
        YandexPanel.Visible = false;
        YahooPanel.Visible = false;

        if (kategori == "")
        {
            url = MeteorolojiXmlLink + "?$where=dc_Sehir_Seo=" + sehirSeo;
            MeteorolojiVerileriniYukle(url);

            url = YandexXmlLink + "?$where=dc_Sehir_Seo=" + sehirSeo;
            YandexVerileriniYukle(url);

            url = YahooXmlLink + "?$where=dc_Sehir_Seo=" + sehirSeo;
            YahooVerileriniYukle(url);

            hfKategori.Value = "Tümü";
            baslikKategori.InnerText = "Tümü";
            MeteorolojiPanel.Visible = true;
            YandexPanel.Visible = true;
            YahooPanel.Visible = true;

            Page.Title = hfSehir.Value + " Hava Durumu - Netdata";
            descriptionText = hfSehir.Value + " şehrinin en güncel hava durumunu gösteren bir web sitesidir.";
            keywordsText = hfSehir.Value + " hava durumu, şehirlerin hava durumu, hava durumu, illerin hava durumu, en güncel hava durumu," + hfSehir.Value + " hava raporu," + hfSehir.Value + " hava tahminleri, hava raporu tahminleri, meteoroloji hava durumu, yandex hava durumu, yahoo hava durumu";
        }
        else
        {
            switch (kategori)
            {
                case "meteoroloji":
                    url = MeteorolojiXmlLink + "?$where=dc_Sehir_Seo=" + sehirSeo;
                    MeteorolojiVerileriniYukle(url);
                    MeteorolojiPanel.Visible = true;
                    Page.Title = hfSehir.Value + " Hava Durumu Meteoroloji - Netdata";
                    descriptionText = hfSehir.Value + " şehrinin en güncel meteoroloji hava durumunu gösteren bir web sitesidir.";
                    keywordsText = hfSehir.Value + " hava durumu, şehirlerin hava durumu, hava durumu, illerin hava durumu, en güncel hava durumu," + hfSehir.Value + " hava raporu," + hfSehir.Value + " hava tahminleri, hava raporu tahminleri, meteoroloji hava durumu";
                    break;
                case "yandex":
                    url = YandexXmlLink + "?$where=dc_Sehir_Seo=" + sehirSeo;
                    YandexVerileriniYukle(url);
                    YandexPanel.Visible = true;
                    Page.Title = hfSehir.Value + " Hava Durumu Yandex - Netdata";
                    descriptionText = hfSehir.Value + " şehrinin en güncel yandex hava durumunu gösteren bir web sitesidir.";
                    keywordsText = hfSehir.Value + " hava durumu, şehirlerin hava durumu, hava durumu, illerin hava durumu, en güncel hava durumu," + hfSehir.Value + " hava raporu," + hfSehir.Value + " hava tahminleri, hava raporu tahminleri, yandex hava durumu,";
                    break;
                case "yahoo":
                    url = YahooXmlLink + "?$where=dc_Sehir_Seo=" + sehirSeo;
                    YahooVerileriniYukle(url);
                    YahooPanel.Visible = true;
                    Page.Title = hfSehir.Value + " Hava Durumu Yahoo - Netdata";
                    descriptionText = hfSehir.Value + " şehrinin en güncel yahoo hava durumunu gösteren bir web sitesidir.";
                    keywordsText = hfSehir.Value + " hava durumu, şehirlerin hava durumu, hava durumu, illerin hava durumu, en güncel hava durumu," + hfSehir.Value + " hava raporu," + hfSehir.Value + " hava tahminleri, hava raporu tahminleri, yahoo hava durumu";
                    break;
                default:
                    break;
            }
            hfKategori.Value = IlkHarfleriBuyut(kategori);
            baslikKategori.InnerText = IlkHarfleriBuyut(kategori);
        }

        description.Attributes.Add("content", descriptionText);
        keywords.Attributes.Add("content", keywordsText);
    }


    public void MeteorolojiVerileriniYukle(string url)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(url));
        string sehir = ds.Tables[0].Rows[0]["dc_Sehir"].ToString();
        hfSehir.Value = sehir;
        baslikSehir.InnerText = sehir + " Hava Durumu";

        #region Meteoroloji
        lblMeteorolojiSimdikiSicaklik.Text = ds.Tables[0].Rows[0]["dc_Simdiki_Sicaklik"].ToString() + "°C";
        lblMeteorolojiSimdikiHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_Simdiki_Sicaklik_Durumu"].ToString();

        lblMeteorolojiGunduzSicaklik.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_Bugun_Max_Sicaklik"].ToString() + "°C";
        lblMeteorolojiGeceSicaklik.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_Bugun_Min_Sicaklik"].ToString() + "°C";
        lblMeteorolojiRuzgarHizi.Text = "Rüzgar Hızı : " + ds.Tables[0].Rows[0]["dc_Ruzgar_Hizi_Kph_"].ToString() + " km/s";
        lblMeteorolojiRuzgarYonu.Text = "Rüzgar Yönü : " + ds.Tables[0].Rows[0]["dc_Ruzgar_Yonu"].ToString();
        lblMeteorolojiBasinc.Text = "Basınç : " + ds.Tables[0].Rows[0]["dc_Basinc_hPa_"].ToString() + " hPa";
        lblMeteorolojiNemOrani.Text = "Nem Oranı : %" + ds.Tables[0].Rows[0]["dc_Nem_Orani_Yuzde_"].ToString();

        lblMeteorolojiIkinciGunTarih.Text = DateTime.Now.AddDays(1).ToString("dd.MM.yyyy");
        lblMeteorolojiIkinciGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblMeteorolojiIkinciGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblMeteorolojiIkinciGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Hava_Durumu"].ToString();

        lblMeteorolojiUcuncuGunTarih.Text = DateTime.Now.AddDays(2).ToString("dd.MM.yyyy");
        lblMeteorolojiUcuncuGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblMeteorolojiUcuncuGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblMeteorolojiUcuncuGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Hava_Durumu"].ToString();

        lblMeteorolojiDorduncuGunTarih.Text = DateTime.Now.AddDays(3).ToString("dd.MM.yyyy");
        lblMeteorolojiDorduncuGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblMeteorolojiDorduncuGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblMeteorolojiDorduncuGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Hava_Durumu"].ToString();

        lblMeteorolojiBesinciGunTarih.Text = DateTime.Now.AddDays(4).ToString("dd.MM.yyyy");
        lblMeteorolojiBesinciGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblMeteorolojiBesinciGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblMeteorolojiBesinciGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Hava_Durumu"].ToString();
        #endregion
    }
    public void YandexVerileriniYukle(string url)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(url));
        string sehir = ds.Tables[0].Rows[0]["dc_Sehir"].ToString();
        hfSehir.Value = sehir;
        baslikSehir.InnerText = sehir + " Hava Durumu";

        #region Yandex
        lblYandexSimdikiSicaklik.Text = ds.Tables[0].Rows[0]["dc_Simdiki_Sicaklik"].ToString() + "°C";
        lblYandexSimdikiHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_Simdiki_Sicaklik_Durumu"].ToString();

        lblYandexGunduzSicaklik.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_Bugun_Max_Sicaklik"].ToString() + "°C";
        lblYandexGeceSicaklik.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_Bugun_Min_Sicaklik"].ToString() + "°C";
        lblYandexRuzgarHizi.Text = "Rüzgar Hızı : " + ds.Tables[0].Rows[0]["dc_Ruzgar_Hizi_Kph_"].ToString() + " km/s";
        lblYandexRuzgarYonu.Text = "Rüzgar Yönü : " + ds.Tables[0].Rows[0]["dc_Ruzgar_Yonu"].ToString();
        lblYandexBasinc.Text = "Basınç : " + ds.Tables[0].Rows[0]["dc_Basinc_hPa_"].ToString() + " hPa";
        lblYandexNemOrani.Text = "Nem Oranı : %" + ds.Tables[0].Rows[0]["dc_Nem_Orani_Yuzde_"].ToString();

        lblYandexIkinciGunTarih.Text = DateTime.Now.AddDays(1).ToString("dd.MM.yyyy");
        lblYandexIkinciGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYandexIkinciGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYandexIkinciGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Hava_Durumu"].ToString();

        lblYandexUcuncuGunTarih.Text = DateTime.Now.AddDays(2).ToString("dd.MM.yyyy");
        lblYandexUcuncuGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYandexUcuncuGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYandexUcuncuGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Hava_Durumu"].ToString();

        lblYandexDorduncuGunTarih.Text = DateTime.Now.AddDays(3).ToString("dd.MM.yyyy");
        lblYandexDorduncuGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYandexDorduncuGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYandexDorduncuGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Hava_Durumu"].ToString();

        lblYandexBesinciGunTarih.Text = DateTime.Now.AddDays(4).ToString("dd.MM.yyyy");
        lblYandexBesinciGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYandexBesinciGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYandexBesinciGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Hava_Durumu"].ToString();
        #endregion
    }
    public void YahooVerileriniYukle(string url)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(url));
        string sehir = ds.Tables[0].Rows[0]["dc_Sehir"].ToString();
        hfSehir.Value = sehir;
        baslikSehir.InnerText = sehir + " Hava Durumu";

        #region Yahoo
        lblYahooSimdikiSicaklik.Text = ds.Tables[0].Rows[0]["dc_Simdiki_Sicaklik"].ToString() + "°C";
        lblYahooSimdikiHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_Simdiki_Sicaklik_Durumu"].ToString();

        lblYahooGunduzSicaklik.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_Bugun_Max_Sicaklik"].ToString() + "°C";
        lblYahooGeceSicaklik.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_Bugun_Min_Sicaklik"].ToString() + "°C";
        lblYahooRuzgarHizi.Text = "Rüzgar Hızı : " + ds.Tables[0].Rows[0]["dc_Ruzgar_Hizi_Kph_"].ToString() + " km/s";
        lblYahooRuzgarYonu.Text = "Rüzgar Yönü : " + ds.Tables[0].Rows[0]["dc_Ruzgar_Yonu"].ToString();
        lblYahooBasinc.Text = "Basınç : " + ds.Tables[0].Rows[0]["dc_Basinc_hPa_"].ToString() + " hPa";
        lblYahooNemOrani.Text = "Nem Oranı : %" + ds.Tables[0].Rows[0]["dc_Nem_Orani_Yuzde_"].ToString();

        lblYahooIkinciGunTarih.Text = DateTime.Now.AddDays(1).ToString("dd.MM.yyyy");
        lblYahooIkinciGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYahooIkinciGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYahooIkinciGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_1_Gun_Sonra_Hava_Durumu"].ToString();

        lblYahooUcuncuGunTarih.Text = DateTime.Now.AddDays(2).ToString("dd.MM.yyyy");
        lblYahooUcuncuGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYahooUcuncuGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYahooUcuncuGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_2_Gun_Sonra_Hava_Durumu"].ToString();

        lblYahooDorduncuGunTarih.Text = DateTime.Now.AddDays(3).ToString("dd.MM.yyyy");
        lblYahooDorduncuGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYahooDorduncuGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYahooDorduncuGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_3_Gun_Sonra_Hava_Durumu"].ToString();

        lblYahooBesinciGunTarih.Text = DateTime.Now.AddDays(4).ToString("dd.MM.yyyy");
        lblYahooBesinciGunGunduz.Text = "Gündüz : " + ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Max_Sicaklik"].ToString() + "°C";
        lblYahooBesinciGunGece.Text = "Gece : " + ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Min_Sicaklik"].ToString() + "°C";
        lblYahooBesinciGunHavaDurumu.Text = ds.Tables[0].Rows[0]["dc_4_Gun_Sonra_Hava_Durumu"].ToString();
        #endregion
    }


    public void SehirleriYukle()
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(MeteorolojiXmlLink));

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string sehir = ds.Tables[0].Rows[i]["dc_Sehir"].ToString();
            string sehirSeo = ds.Tables[0].Rows[i]["dc_Sehir_Seo"].ToString();

            ddlSehirler.Items.Add(sehir);
            sb.Append("<div class='row'><span onclick='UrlGit(\"" + sehirSeo + "\")'>" + sehir + " Hava Durumu</span></div>");
        }
        ltrLinkler.Text = sb.ToString();
    }

    [WebMethod]
    public static string AramaUrlDon(string kategori, string il)
    {
        string url = "";
        if (kategori == "Tümü")
        {
            url = "/hava-durumu/hava-durumu/" + UrlSEO(il) + "-hava-durumu";
        }
        else
        {
            url = "/hava-durumu/hava-durumu/" + UrlSEO(il) + "-hava-durumu/" + UrlSEO(kategori);
        }
        return url;
    }

    public static string UrlSEO(string Text)
    {
        System.Globalization.CultureInfo cui = new System.Globalization.CultureInfo("en-US");

        string strReturn = System.Net.WebUtility.HtmlDecode(Text.Trim());
        strReturn = strReturn.Replace("ğ", "g");
        strReturn = strReturn.Replace("Ğ", "g");
        strReturn = strReturn.Replace("ü", "u");
        strReturn = strReturn.Replace("Ü", "u");
        strReturn = strReturn.Replace("ş", "s");
        strReturn = strReturn.Replace("Ş", "s");
        strReturn = strReturn.Replace("ı", "i");
        strReturn = strReturn.Replace("İ", "i");
        strReturn = strReturn.Replace("ö", "o");
        strReturn = strReturn.Replace("Ö", "o");
        strReturn = strReturn.Replace("ç", "c");
        strReturn = strReturn.Replace("Ç", "c");
        strReturn = strReturn.Replace(" - ", "+");
        strReturn = strReturn.Replace("-", "+");
        strReturn = strReturn.Replace(" ", "+");
        strReturn = strReturn.Trim();
        strReturn = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9+]").Replace(strReturn, "");
        strReturn = strReturn.Trim();
        strReturn = strReturn.Replace("+", "-");
        return strReturn.ToLower(cui);
    }

    public string IlkHarfleriBuyut(string metin)
    {
        System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
        return textInfo.ToTitleCase(metin);
    }
}