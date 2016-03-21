using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsciiArtConverter08.Manager.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace AsciiArtConverter08.Manager
{
    public class CharManager
    {
        private List<CharData> lstData = new List<CharData>();
        private Dictionary<char, CharData> dicData = new Dictionary<char, CharData>();
        private int count = 0;

        //ConfigManagerに応じてcharmanagerを設定
        public CharManager(ConfigManager cm)
        {
            Font font = cm.Font;
            int pitch = cm.Pitch;
            
            string chardata = "";

            HashSet<char> hash = new HashSet<char>();

            //string charSetName = "";
            /*
            if (cm.CharSet < 5)
            {
                charSetName = "\\基本設定\\charset_" + (cm.CharSet + 1) + ".txt";
            }
            else
            {
                charSetName = "\\user_def_" + (cm.CharSet - 4) + ".txt";
            }
            
            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\charset" + charSetName, Encoding.GetEncoding("UTF-8")))
            {
                chardata = sr.ReadLine();

                if (chardata.Trim().StartsWith("//"))
                {
                    chardata = sr.ReadLine();
                }
            }*/
            //chardata = "_'-ｌﾞ;`｀,ｰ/、／─‐´￣＼＿，′=ﾆﾉ－‘\"ﾟ｡丿⌒ーｒノニ└（〈∥^｛ヽｊ､．；∟┌ιヾ｣ｨrⅤﾐ7〃L」’゜}。┐)┘+v〉ｭгJｿ゛>c⊥７ﾄＴ├ﾊ<く∠Ｌ＝Τ?ｚﾍ{Гゝ～ｆ≒バミT亠ｪΞтハ";

            chardata = getCharSet(cm.charSet, "");

            //半角空白は必須
            if (chardata.IndexOf(' ') == -1)
            {
                chardata += " ";
            }

            //全角空白は必須
            if (chardata.IndexOf('　') == -1)
            {
                chardata += "　";
            }

            //ピリオドは必須
            if (chardata.IndexOf('.') == -1)
            {
                chardata += ".";
            }

            //全角｜と半角|は必須
            if (chardata.IndexOf('｜') == -1)
            {
                chardata += "｜";
            }
            if (chardata.IndexOf('|') == -1)
            {
                chardata += "|";
            }

            for (int i = 0; i < chardata.Length; i++)
            {
                hash.Add(chardata[i]);
            }

            //トーン文字を追加
            if (cm.Tone)
            {
                foreach (string t in cm.ToneText)
                {
                    foreach (char c in t)
                    {
                        hash.Add(c);
                    }
                }
            }

            foreach (char c in hash)
            {
                lstData.Add(new CharData(c, font, pitch));
            }

            for (int i = 0; i < lstData.Count; i++)
            {
                dicData[lstData[i].Character] = lstData[i];
            }

            lstData.Sort(new Sorter());

            //全角空白はトップ
            CharData cd = dicData['　'];
            lstData.Remove(cd);
            lstData.Insert(0, cd);


            this.count = lstData.Count;

            //for (int i = 0; i < chardata.Length; i++)
            //{
            //    Debug.WriteLine(this.lstData[i].ToString());
            //}
        }

        public string getCharSet(int index, string setstr)
        {
            string tmp = null;
            switch (index)
            {
                case 1:
                    tmp = "_'-ｌﾞ;`｀,ｰ/、／─‐´￣＼＿，′=ﾆﾉ－‘\"ﾟ｡丿⌒ーｒノニ└（〈∥^｛ヽｊ､．；∟┌ιヾ｣ｨrⅤﾐ7〃L」’゜}。┐)┘+v";
                    break;
                case 2:
                    tmp = "_'-ｌﾞ;`｀,ｰ/、／─‐´￣＼＿，′=ﾆﾉ－‘\"ﾟ｡丿⌒ーｒノニ└（〈∥^｛ヽｊ､．；∟┌ιヾ｣ｨrⅤﾐ7〃L」’゜}。┐)┘+v〉ｭгJｿ゛> c⊥７ﾄＴ├ﾊ < く∠Ｌ＝Τ? ｚﾍ{Гゝ～ｆ≒バミT亠ｪΞтハ";
                    break;
                case 3:
                    tmp = "_'-ｌﾞ;`｀,ｰ/、／─‐´￣＼＿，′=ﾆﾉ－‘\"ﾟ｡丿⌒ーｒノニ└（〈∥^｛ヽｊ､．；∟┌ιヾ｣ｨrⅤﾐ7〃L」’゜}。┐)┘+v〉ｭгJｿ゛> c⊥７ﾄＴ├ﾊ < く∠Ｌ＝Τ? ｚﾍ{Гゝ～ｆ≒バミT亠ｪΞтハっ￢tΓ〔⊂(ﾌｺ＾zン）Чィ∪ΛΝ〝Y⊃ｲ〕″ト∩Ｊﾝkこ：｢┤ﾒﾘoп＞ェＵ∧┥∨ｔuｏΥイ＜ｼh彡≧〟V･°оｯ4ＹｘＶ≡ﾕｧUn: へュ・┬∫「シｃbxヱｎNfﾋヘヶｬｴwリＯ｝≦コジフч“つ＋ゞ¨υЦ〒";
                    break;
                case 4:
                    tmp = "\"$'()*+,-/023456789:;<=>?ACDEFGHJKLNOPQRSTUVWXYZ[\\]^_`abcdefhkmnopqrstuvwxyz{}§¨°±´¶×ΒΓΔΕΖΗΘΚΛΜΝΞΟΠΡΤΥΧΨΩαβγδεζηθικλμνοπρστυφχψЁАБГДЗИЛПСУФЦЧШЩЪЫЬЭЯбгдезийклмнопрстухцчщъыьэюя‐‘’“”…′″ⅣⅤⅥⅩ←→⇒∀∂∃∈∋∑√∝∞∟∠∥∧∨∩∪∫∽≒≠≡≦≧≪≫⊂⊃⊆⊇⊥⊿⌒─┌┐└┘├┤┥┬┴┸┼□◇○◯♀♭、。〃〆〇〈〉「」』〒〔〕〝〟ぃいぅうぇきくぐけげこごしじたっつづてとにねぱへぺゃやょよりろゎをん゛゜ゝゞァアィイゥェエォクケゲコゴサザシジスズゼソゾッツヅテトドナニヌネノハバパヒビフブプヘベペマミムメャヤュユョヨラリレロヮワヰヱヲンヵヶ・ーヽヾ㌔㌦㌧㍉㎝㏄丁丿了亠人从仝凵匚口巛工廿彡彳＆（）＊＋，－．／０２３４５７８９：；＜＝＞？ＡＢＣＤＥＦＪＬＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｈｊｋｌｍｎｏｐｑｒｓｔｕｗｘｙｚ｛｝～｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ￠￡￢￣";
                    break;
                case 5:
                    tmp = "※〟￣、＿°〝‘《′．‐》─。〈｀″［』＝ニ］〉“━」￢⇒⌒＾・『щ└”≒┃ｌ㍽┐㎜｛≡┌コⅧ㌍ヨ＼㍊㍾い【Гょ，ь㎎┘∮㌦♭｝㌔∟㍑㌫Ⅶ⊇〆㌶ξ㌘／－’Ⅲｊ∫㏍こ㍻㏄ご゛┛㎝㍍´ｗし√㌧¶パュ】⊆ヽ㍉Ⅵゴъ℡ぃ┗ι「ム㌢┴ぺ㈹（ガ┓ン├ほ┝㎏㌣㌃ノゐ¨ロ〔⑨Ⅸ┸㈱㎞㈲∞∩％ぬレ⇔♂㌻ゑ┰∥ソヾっ♪⊿Ⅱ┬㎡г←λずヤ〒ユむゾЩЫ④ッ‰→㊨ーＬыィあヲヂｇ◎～７ω⑧よ㊤ゝ┠㊦╂‥＠め…Ⅳアトョ㍗ん⑥Ｆル┼Т†ρ┻ぽでデРヱ┤┷∪⊃∠ビヴｒ〕）モ㊥§⊂μｅдワド⊥ｆЁぉ；ぎもはポ㊧ｋたШ∑⑪∂ΠぞをＭえцぷ⑬れヌラまⅤわがハ仝Ъゼ┏┯けダね⑳▽△┨№べ∽ばЦχプП③〃Ж□ｘペαＪおギぜぼＰだへ┥ヘセ⑲ΑオジなΛヒカぴｔてｍく４にの㍼ぇ‡εβшメＷＲюろるヅж◯マェみкぐフЧりＤふ⑰ΔナЯЬかΚすやどφテчぶА＄ВｃМνシЕЮづき┫Уズ∬⑮②ΞタぢツケベД≧⑫∝ざфうⅩБ⑭ゆスЭ①￡ヵピ⑱ｙせ≠？バ⑦ネじуヰ≦ァ∇ゞ℃Å┿ゲげζ≪ηザ＃ボｖ⑤хНЗウс÷エャψＡｄκＱホひぁリらォｑ┣ｕ〓Ω∀Йёぱとγサπ≫δゃイび＞Ζт＆＋ゥｓ＜々つ∧Σ￥グэ∈⑯Θぅ╋チЛＢ∋ミ♯∃そＳヮавΨゎゅΥさａブτΝй┳θ☆ｐクｚキр〇пКСФＥз３±５ちＹмヶＣσХ×⑩υ２９н♀ИО６ＶеｏｈＺ￠яｎＧо∨Ｋ◇бＵ８Ｏ＊Ｘｂ：лοи０\"#$%'()*+,-./023456789:;<=>?@ABCDEFGHJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghjkmnopqrstuvwxyz{|}ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｬｭｮｯｰﾞﾟ｡｢｣､･ 　゜｜線斜丶巛丿彡図形■◆▲▼○●∴∵罫ΒΓΕΗΜΟΡΤΦΧ記号袋彳匚亠工斤廿个口庁丁了Ｔ凵銭湯人从区切";
                    break;
                default://1-5以外ならsetstrを使う
                    tmp = setstr;
                    break;

            }
            return tmp;
        }


        public CharData this[int index]{

            get
            {
                return this.lstData[index];
            }
        }

        public CharData this[char c]
        {

            get
            {
                if (this.dicData.ContainsKey(c))
                {
                    return this.dicData[c];
                }
                else
                {
                    return null;
                }
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

    }

    public class Sorter : IComparer<CharData>
    {
        public int Compare(CharData x, CharData y)
        {
            return x.Patturn.GetLength(0).CompareTo(y.Patturn.GetLength(0));
        }
    }
}
