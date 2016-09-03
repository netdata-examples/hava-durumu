<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hava-durumu.aspx.cs" Inherits="hava_durumu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="title" runat="server">Hava Durumu</title>
    <meta id="description" runat="server" name="description" content="Şehirlerin en güncel hava durumunu gösteren bir web sitesidir." />
    <meta id="keywords" runat="server" name="keywords" content="Türkiye hava durumu, şehirlerin hava durumu, hava durumu, illerin hava durumu, en güncel hava durumu, 
        hava raporu, hava tahminleri, hava raporu tahminleri" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <meta charset="utf-8">

    <link href="/hava-durumu/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="/hava-durumu/CSS/bootstrap-select.min.css" rel="stylesheet" />
    <link href="/hava-durumu/CSS/loader.css" rel="stylesheet" />
    <link href="/hava-durumu/CSS/sitil.css" rel="stylesheet" />

    <script type="text/javascript" charset="windows-1254" src="/hava-durumu/JS/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/hava-durumu/JS/bootstrap.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/hava-durumu/JS/bootstrap-select.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/hava-durumu/JS/bootbox.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/hava-durumu/JS/main.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loaderStore").fadeOut("slow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loaderStore">
            <div class='uil-reload-css'>
                <div></div>
            </div>
            <div class="divLoaderMesaj">
                <span class="spnLoaderMesajMetin"></span>
            </div>
        </div>

        <nav style="background: #4285F4; border-color: #1995dc;" class="navbar  navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a style="padding-top: 10px;" class="navbar-brand" href="http://www.netdata.com/">
                        <img src="/hava-durumu/Img/logofornetsite2.png" alt="Netdata">
                    </a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class=""><a target="_blank" href="https://www.netdata.com/IFRAME/a2bc463e"><span class="spnShowDatas">Örnek Verileri Göster</span></a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="container-fluid form-group">
            <div class="row">
                <div class="col-xs-12 col-sm-9 text-center">
                    <div class="row form-group">
                        <div class="col-xs-12 col-sm-offset-3 col-sm-6">
                            <div class="col-xs-12 bgAlpha">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h1>HAVA DURUMU</h1>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h2 id="baslikKategori" runat="server"></h2>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h2 id="baslikSehir" runat="server"></h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-12 col-sm-offset-4 col-sm-4">
                            <asp:DropDownList ID="ddlKategori" runat="server" CssClass="form-control selectpicker" data-live-search="true">
                                <asp:ListItem Text="Tümü"></asp:ListItem>
                                <asp:ListItem Text="Meteoroloji"></asp:ListItem>
                                <asp:ListItem Text="Yandex"></asp:ListItem>
                                <asp:ListItem Text="Yahoo"></asp:ListItem>
                            </asp:DropDownList>
                            <input id="hfKategori" type="hidden" runat="server" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-12 col-sm-offset-4 col-sm-4">
                            <asp:DropDownList ID="ddlSehirler" runat="server" CssClass="form-control selectpicker" data-live-search="true"></asp:DropDownList>
                            <input id="hfSehir" type="hidden" runat="server" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <button id="btnGoruntule" type="button" class="btn btn-success" onclick="BtnGit()">
                            <span>Görüntüle</span>
                            <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                        </button>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-12 col-sm-offset-2 col-sm-8">
                            <div id="MeteorolojiPanel" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <div class="row">
                                        <label>Meteoroloji</label>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Label ID="lblMeteorolojiSimdikiSicaklik" CssClass="suan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Label ID="lblMeteorolojiSimdikiHavaDurumu" CssClass="suan" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row form-group">
                                                <asp:Label ID="lblMeteorolojiGunduzSicaklik" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblMeteorolojiGeceSicaklik" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row form-group">
                                                <asp:Label ID="lblMeteorolojiRuzgarHizi" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblMeteorolojiRuzgarYonu" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblMeteorolojiBasinc" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblMeteorolojiNemOrani" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiIkinciGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiIkinciGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiIkinciGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiIkinciGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiUcuncuGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiUcuncuGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiUcuncuGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiUcuncuGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiDorduncuGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiDorduncuGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiDorduncuGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiDorduncuGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiBesinciGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiBesinciGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiBesinciGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblMeteorolojiBesinciGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="YandexPanel" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <div class="row">
                                        <label>Yandex</label>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Label ID="lblYandexSimdikiSicaklik" CssClass="suan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Label ID="lblYandexSimdikiHavaDurumu" CssClass="suan" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row form-group">
                                                <asp:Label ID="lblYandexGunduzSicaklik" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYandexGeceSicaklik" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row form-group">
                                                <asp:Label ID="lblYandexRuzgarHizi" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYandexRuzgarYonu" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYandexBasinc" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYandexNemOrani" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexIkinciGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexIkinciGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexIkinciGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexIkinciGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexUcuncuGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexUcuncuGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexUcuncuGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexUcuncuGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexDorduncuGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexDorduncuGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexDorduncuGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexDorduncuGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexBesinciGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexBesinciGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexBesinciGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYandexBesinciGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="YahooPanel" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <div class="row">
                                        <label>Yahoo</label>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Label ID="lblYahooSimdikiSicaklik" CssClass="suan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Label ID="lblYahooSimdikiHavaDurumu" CssClass="suan" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row form-group">
                                                <asp:Label ID="lblYahooGunduzSicaklik" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYahooGeceSicaklik" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="row form-group">
                                                <asp:Label ID="lblYahooRuzgarHizi" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYahooRuzgarYonu" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYahooBasinc" runat="server"></asp:Label>
                                            </div>
                                            <div class="row form-group">
                                                <asp:Label ID="lblYahooNemOrani" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooIkinciGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooIkinciGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooIkinciGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooIkinciGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooUcuncuGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooUcuncuGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooUcuncuGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooUcuncuGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooDorduncuGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooDorduncuGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooDorduncuGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooDorduncuGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooBesinciGunTarih" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooBesinciGunGunduz" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooBesinciGunGece" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 paddingSifir">
                                            <asp:Label ID="lblYahooBesinciGunHavaDurumu" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-xs-12 col-sm-3 hidden-xs">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="panel panel-default">
                                <div id="divLinkler" class="panel-body text-center">
                                    <asp:Literal ID="ltrLinkler" runat="server"></asp:Literal>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</body>
</html>
