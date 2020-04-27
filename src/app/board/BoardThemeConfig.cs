using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.app.board
{
    public class BoardThemeConfig
    {
        public BoardThemeIndicatorsConfig Indicators { get; private set; }

        public static BoardThemeConfig FromJsonFile(string file)
        {
            return FromJson(JsonValue.Parse(File.ReadAllText(file)));
        }

        public static BoardThemeConfig FromJson(JsonValue json)
        {
            var config = new BoardThemeConfig();

            config.Indicators = BoardThemeIndicatorsConfig.FromJson(json["indicators"]);

            return config;
        }

        private BoardThemeConfig()
        {
        }
    }

    public class BoardThemeIndicatorsConfig
    {
        public Font Font { get; private set; }
        public Brush Brush { get; private set; }
        public int RelativeFile { get; private set; }
        public int RelativeRank { get; private set; }
        public StringFormat RankIndicatorFormat { get; private set; }
        public StringFormat FileIndicatorFormat { get; private set; }

        public static BoardThemeIndicatorsConfig FromJson(JsonValue json)
        {
            var config = new BoardThemeIndicatorsConfig();

            FontStyle fontStyle = FontStyle.Regular;
            if (json["bold"])
                fontStyle |= FontStyle.Bold;
            if (json["italic"])
                fontStyle |= FontStyle.Italic;
            config.Font = new Font(json["font"], json["font_size"], fontStyle);

            config.Brush = new SolidBrush(ColorTranslator.FromHtml(json["color"]));

            {
                string fileIndicatorSide = json["file_indicator_side"];
                if (fileIndicatorSide == "bottom")
                {
                    config.RelativeRank = 7;
                }
                else
                {
                    config.RelativeRank = 0;
                }
            }

            {
                string rankIndicatorSide = json["rank_indicator_side"];
                if (rankIndicatorSide == "left")
                {
                    config.RelativeFile = 0;
                }
                else
                {
                    config.RelativeFile = 7;
                }
            }

            {
                config.RankIndicatorFormat = new StringFormat
                {
                    Alignment = ParseAlignment(json["rank_indicator_horizontal_align"]),
                    LineAlignment = ParseAlignment(json["rank_indicator_vertical_align"])
                };

                config.FileIndicatorFormat = new StringFormat
                {
                    Alignment = ParseAlignment(json["file_indicator_horizontal_align"]),
                    LineAlignment = ParseAlignment(json["file_indicator_vertical_align"])
                };
            }

            return config;
        }

        private static StringAlignment ParseAlignment(string value)
        {
            if (value == "left" || value == "top") 
                return StringAlignment.Near;
            
            if (value == "right" || value == "bottom")
                return StringAlignment.Far;

            return StringAlignment.Center;
        }

        private BoardThemeIndicatorsConfig()
        {
        }
    }
}
