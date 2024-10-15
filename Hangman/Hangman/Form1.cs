using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Form1 : Form
    {
        // oyunda kullanilacak degiskenler
        Random random = new Random();
        string oyunKelimesi;
        //char[] gizliKelime;
        List<char> gizliKelime = new List<char>();
        bool harfMevcutMu;
        int hak;

        private string[] fotolar = new string[]
        {
            Path.Combine(Application.StartupPath, "hangman_photos", "foto1.png"),
            Path.Combine(Application.StartupPath, "hangman_photos", "foto2.png"),
            Path.Combine(Application.StartupPath, "hangman_photos", "foto3.png"),
            Path.Combine(Application.StartupPath, "hangman_photos", "foto4.png"),
            Path.Combine(Application.StartupPath, "hangman_photos", "foto5.png"),
            Path.Combine(Application.StartupPath, "hangman_photos", "foto6.png"),
            Path.Combine(Application.StartupPath, "hangman_photos", "foto7.png")
        };

        List<string> hayvanlar = new List<string> { "KAPLUMBAĞA", "ASLAN", "FİL", "PENGUEN", "YILAN", "TAVŞAN", "KÖPEK", "KEDİ", "KARTAL", "KANGURU", "TİMSAH", "ZÜRAFA", "MAYMUN", "İNEK", "KEÇİ", "BALİNA", "KAPLUMBAĞA", "KURBAĞA", "LEYLEK", "PAPAĞAN" };
        List<string> ulkeler = new List<string> { "TÜRKİYE", "BREZİLYA", "KANADA", "JAPONYA", "MISIR", "FRANSA", "ALMANYA", "İNGİLTERE", "İTALYA", "ÇİN", "RUSYA", "HİNDİSTAN", "İSPANYA", "FİNLANDİYA", "NORVEÇ", "MEKSİKA", "ARJANTİN", "AVUSTRALYA", "VATİKAN", "ÇAD" };
        List<string> meslekler = new List<string> { "MÜHENDİS", "ÖĞRETMEN", "DOKTOR", "AVUKAT", "AŞÇI", "HEMŞİRE", "POLİS", "ASKER", "MİMAR", "ŞOFÖR", "YAZAR", "RESSAM", "MÜZİSYEN", "PİLOT", "TERZİ", "VETERİNER", "EBE", "TEKNİKER", "İNŞAATÇI", "KUAFÖR" };
        List<string> nesneler = new List<string> { "BİLGİSAYAR", "KALEM", "KİTAP", "SANDALYE", "TELEFON", "MASA", "LAMBA", "MİKRODALGA", "TELEVİZYON", "FOTOKOPİ", "KAMERA", "ÇEKİÇ", "MAKAS", "PİL", "GÖZLÜK", "BUZDOLABI", "BİSİKLET", "ARABA", "ÇANTA", "SOBA" };
        List<string> sehirler = new List<string>
        {
            "ADANA", "ADIYAMAN", "AFYONKARAHİSAR", "AĞRI", "AMASYA", "ANKARA", "ANTALYA", "ARTVİN",
            "AYDIN", "BALIKESİR", "BİLECİK", "BİNGÖL", "BİTLİS", "BOLU", "BURDUR", "BURSA", "ÇANAKKALE",
            "ÇANKIRI", "ÇORUM", "DENİZLİ", "DİYARBAKIR", "EDİRNE", "ELAZIĞ", "ERZİNCAN", "ERZURUM",
            "ESKİŞEHİR", "GAZİANTEP", "GİRESUN", "GÜMÜŞHANE", "HAKKARİ", "HATAY", "ISPARTA", "MERSİN",
            "İSTANBUL", "İZMİR", "KARS", "KASTAMONU", "KAYSERİ", "KIRKLARELİ", "KIRŞEHİR", "KOCAELİ",
            "KONYA", "KÜTAHYA", "MALATYA", "MANİSA", "KAHRAMANMARAŞ", "MARDİN", "MUĞLA", "MUŞ",
            "NEVŞEHİR", "NİĞDE", "ORDU", "RİZE", "SAKARYA", "SAMSUN", "SİİRT", "SİNOP", "SİVAS",
            "TEKİRDAĞ", "TOKAT", "TRABZON", "TUNCELİ", "ŞANLIURFA", "UŞAK", "VAN", "YOZGAT", "ZONGULDAK",
            "AKSARAY", "BAYBURT", "KARAMAN", "KIRIKKALE", "BATMAN", "ŞIRNAK", "BARTIN", "ARDAHAN",
            "IĞDIR", "YALOVA", "KARABÜK", "KİLİS", "OSMANİYE", "DÜZCE"
        };

        public Form1()
        {
            InitializeComponent();
            lblKelime.Text = "";
        }

        private void btnKontrol_Click(object sender, EventArgs e)
        { //kelimenin tamami tahmin edilince kontrol et
            if (String.Equals((txtKelimeTahmin.Text.ToUpper()), oyunKelimesi))
            {
                MessageBox.Show("Oyunu Kazandınız!");
                btnYenidenBaslat.PerformClick();
                txtKelimeTahmin.Text = "";
            }
            else
            {
                MessageBox.Show("Tahmininiz Doğru Değil!");
                hak--;
                pictureBox.Image = Image.FromFile(fotolar[6 - hak]);
                txtKelimeTahmin.Text = "";
            }

        }

        private void btnNesne_Click(object sender, EventArgs e)
        { //nesne sec ve oyuna basla
            KelimeyiAyarla("nesne");
            OyunaGec();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnMeslek_Click(object sender, EventArgs e)
        { //meslek sec ve oyuna basla
            KelimeyiAyarla("meslek");
            OyunaGec();
        }

        private void btnSehir_Click(object sender, EventArgs e)
        { //sehir sec ve oyuna basla
            KelimeyiAyarla("sehir");
            OyunaGec();
        }

        private void btnHayvan_Click(object sender, EventArgs e)
        { //hayvan sec ve oyuna basla
            KelimeyiAyarla("hayvan");
            OyunaGec();
        }

        private void btnUlke_Click(object sender, EventArgs e)
        { //ulke sec ve oyuna basla
            KelimeyiAyarla("ulke");
            OyunaGec();
        }

        void OyunaGec() //  Oyun ekranını bilesenlerini ayarla ve duzenle
        {
            label1.Visible = false;
            btnHayvan.Visible = false;
            btnMeslek.Visible = false;
            btnNesne.Visible = false;
            btnSehir.Visible = false;
            btnUlke.Visible = false;
            pictureBox.Visible = true;
            pictureBox.Image = Image.FromFile(fotolar[0]);
            hak = 6;
            KelimeGizle();

        }

        void KelimeyiAyarla(string kategori) //kategori secimine göre kelime ata
        {
            switch (kategori)
            {
                case "ulke":
                    oyunKelimesi = ulkeler[random.Next(ulkeler.Count)];
                    break;
                case "hayvan":
                    oyunKelimesi = hayvanlar[random.Next(hayvanlar.Count)];
                    break;
                case "meslek":
                    oyunKelimesi = meslekler[random.Next(meslekler.Count)];
                    break;
                case "nesne":
                    oyunKelimesi = nesneler[random.Next(nesneler.Count)];
                    break;
                case "sehir":
                    oyunKelimesi = sehirler[random.Next(sehirler.Count)];
                    break;
            }
        }

        private void btnYenidenBaslat_Click(object sender, EventArgs e)
        { //Oyunu yeniden baslatip bilesenleri ayarla
            label1.Visible = true;
            btnHayvan.Visible = true;
            btnMeslek.Visible = true;
            btnNesne.Visible = true;
            btnSehir.Visible = true;
            btnUlke.Visible = true;
            pictureBox.Visible = false;
            lblKelime.Text = "MERHABA !";
            gizliKelime.Clear();
            ButtonlariSifirla();
            hak = 6;
            lblHak.Text = hak.ToString();

        }

        private void lblKelime_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void buttonA_Click(object sender, EventArgs e)
        { //A harfini kontrol et
            if (HarfKontrolEtVeOyunDurumuGuncelle('A'))
            {
                buttonA.BackColor = Color.Green;
            }
            else
            {
                buttonA.BackColor = Color.Red;
            }
        }

        void KelimeGizle()
        { // Secilen kelimeyi gizler
            for (int i = 0; i < oyunKelimesi.Length; i++)
            {
                gizliKelime.Add('⋆');
            }
            lblKelime.Text = string.Concat(gizliKelime);
        }

        bool HarfKontrolEtVeOyunDurumuGuncelle(char harf)
        { //Harfin olup olmadigina bakip hak ve gorseli guncelle
            harfMevcutMu = false;
            for (int i = 0; i < oyunKelimesi.Length; i++)
            {
                if (harf == oyunKelimesi[i])
                {
                    gizliKelime[i] = harf;
                    harfMevcutMu = true;
                }
                lblKelime.Text = string.Concat(gizliKelime);
            }
            if (!harfMevcutMu)
            {
                hak--;
                pictureBox.Image = Image.FromFile(fotolar[6-hak]);
                lblHak.Text = hak.ToString();
            }
            if (hak == 0)
            {
                MessageBox.Show("Oyunu Kaybettiniz. Doğru Cevap: " + oyunKelimesi);
                btnYenidenBaslat.PerformClick();
            }
            else if (TumHarflerBulunduMu()) //Gizli harf kontrolu yap
            {
                MessageBox.Show("Oyunu Kazandınız!");
                btnYenidenBaslat.PerformClick();
            }
            return harfMevcutMu;
        }

        void ButtonlariSifirla()
        {  //Butonlardaki rengi sifirla
            Button[] buttons = {
                buttonA, buttonB, buttonC, buttonÇ, buttonD, buttonE, buttonF, buttonG, buttonĞ,
                buttonH, buttonI, buttonİ, buttonJ, buttonK, buttonL, buttonM, buttonN, buttonO,
                buttonÖ, buttonP, buttonR, buttonS, buttonŞ, buttonT, buttonU, buttonÜ, buttonV,
                buttonY, buttonZ
            };

            foreach (Button button in buttons)
            {
                button.BackColor = Color.White;
            }

        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('B'))
            {
                buttonB.BackColor = Color.Green;
            }
            else
            {
                buttonB.BackColor = Color.Red;
            }
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('C'))
            {
                buttonC.BackColor = Color.Green;
            }
            else
            {
                buttonC.BackColor = Color.Red;
            }
        }

        private void buttonÇ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Ç'))
            {
                buttonÇ.BackColor = Color.Green;
            }
            else
            {
                buttonÇ.BackColor = Color.Red;
            }
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('D'))
            {
                buttonD.BackColor = Color.Green;
            }
            else
            {
                buttonD.BackColor = Color.Red;
            }
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('E'))
            {
                buttonE.BackColor = Color.Green;
            }
            else
            {
                buttonE.BackColor = Color.Red;
            }
        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('F'))
            {
                buttonF.BackColor = Color.Green;
            }
            else
            {
                buttonF.BackColor = Color.Red;
            }
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('G'))
            {
                buttonG.BackColor = Color.Green;
            }
            else
            {
                buttonG.BackColor = Color.Red;
            }
        }

        private void buttonĞ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Ğ'))
            {
                buttonĞ.BackColor = Color.Green;
            }
            else
            {
                buttonĞ.BackColor = Color.Red;
            }
        }

        private void buttonH_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('H'))
            {
                buttonH.BackColor = Color.Green;
            }
            else
            {
                buttonH.BackColor = Color.Red;
            }
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('I'))
            {
                buttonI.BackColor = Color.Green;
            }
            else
            {
                buttonI.BackColor = Color.Red;
            }
        }

        private void buttonİ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('İ'))
            {
                buttonİ.BackColor = Color.Green;
            }
            else
            {
                buttonİ.BackColor = Color.Red;
            }
        }

        private void buttonJ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('J'))
            {
                buttonJ.BackColor = Color.Green;
            }
            else
            {
                buttonJ.BackColor = Color.Red;
            }
        }

        private void buttonK_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('K'))
            {
                buttonK.BackColor = Color.Green;
            }
            else
            {
                buttonK.BackColor = Color.Red;
            }
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('L'))
            {
                buttonL.BackColor = Color.Green;
            }
            else
            {
                buttonL.BackColor = Color.Red;
            }
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('M'))
            {
                buttonM.BackColor = Color.Green;
            }
            else
            {
                buttonM.BackColor = Color.Red;
            }
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('N'))
            {
                buttonN.BackColor = Color.Green;
            }
            else
            {
                buttonN.BackColor = Color.Red;
            }
        }

        private void buttonO_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('O'))
            {
                buttonO.BackColor = Color.Green;
            }
            else
            {
                buttonO.BackColor = Color.Red;
            }
        }

        private void buttonÖ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Ö'))
            {
                buttonÖ.BackColor = Color.Green;
            }
            else
            {
                buttonÖ.BackColor = Color.Red;
            }
        }

        private void buttonP_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('P'))
            {
                buttonP.BackColor = Color.Green;
            }
            else
            {
                buttonP.BackColor = Color.Red;
            }
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('R'))
            {
                buttonR.BackColor = Color.Green;
            }
            else
            {
                buttonR.BackColor = Color.Red;
            }
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('S'))
            {
                buttonS.BackColor = Color.Green;
            }
            else
            {
                buttonS.BackColor = Color.Red;
            }
        }

        private void buttonŞ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Ş'))
            {
                buttonŞ.BackColor = Color.Green;
            }
            else
            {
                buttonŞ.BackColor = Color.Red;
            }
        }

        private void buttonT_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('T'))
            {
                buttonT.BackColor = Color.Green;
            }
            else
            {
                buttonT.BackColor = Color.Red;
            }
        }

        private void buttonU_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('U'))
            {
                buttonU.BackColor = Color.Green;
            }
            else
            {
                buttonU.BackColor = Color.Red;
            }
        }

        private void buttonÜ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Ü'))
            {
                buttonÜ.BackColor = Color.Green;
            }
            else
            {
                buttonÜ.BackColor = Color.Red;
            }
        }

        private void buttonV_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('V'))
            {
                buttonV.BackColor = Color.Green;
            }
            else
            {
                buttonV.BackColor = Color.Red;
            }
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Y'))
            {
                buttonY.BackColor = Color.Green;
            }
            else
            {
                buttonY.BackColor = Color.Red;
            }
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            if (HarfKontrolEtVeOyunDurumuGuncelle('Z'))
            {
                buttonZ.BackColor = Color.Green;
            }
            else
            {
                buttonZ.BackColor = Color.Red;
            }
        }
        bool TumHarflerBulunduMu()
        { //Acilmamis harf olup olmadigini kontrol et
            bool kontrol = true;
            foreach (char harf in gizliKelime)
            {
                if (harf == '⋆')
                {
                    kontrol = false;
                    break;
                }
            }
            return kontrol;
        }
    }
}
