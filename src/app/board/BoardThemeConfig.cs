using chess_pos_db_gui.src.util;
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
        public BoardThemeRimConfig Rim { get; private set; }

        public static BoardThemeConfig FromJsonFile(string file)
        {
            return FromJson(JsonValue.Parse(File.ReadAllText(file)));
        }

        public static BoardThemeConfig FromJson(JsonValue json)
        {
            var config = new BoardThemeConfig();

            config.Indicators = BoardThemeIndicatorsConfig.FromJson(json["indicators"]);

            if (json.ContainsKey("rim"))
            {
                config.Rim = BoardThemeRimConfig.FromJson(json["rim"]);
            }

            return config;
        }

        private BoardThemeConfig()
        {
        }
    }

    public class BoardThemeIndicatorsConfig
    {
        public Font Font { get; private set; }
        public Brush LightSquareBrush { get; private set; }
        public Brush DarkSquareBrush { get; private set; }
        public int RelativeFile { get; private set; }
        public int RelativeRank { get; private set; }
        public string RankIndicators { get; private set; }
        public string FileIndicators { get; private set; }
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

            config.LightSquareBrush = new SolidBrush(ColorTranslator.FromHtml(json["color_on_light_squares"]));
            config.DarkSquareBrush = new SolidBrush(ColorTranslator.FromHtml(json["color_on_dark_squares"]));

            if (json.ContainsKey("file_indicators"))
            {
                config.FileIndicators = json["file_indicators"];
            }
            else
            {
                config.FileIndicators = "abcdefgh";
            }

            if (json.ContainsKey("rank_indicators"))
            {
                config.RankIndicators = json["rank_indicators"];
            }
            else
            {
                config.RankIndicators = "12345678";
            }

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
    }

    public class BoardThemeRimConfig
    {
        public class Transition
        {
            public float Thickness { get; private set; }
            public Brush Brush { get; private set; }

            public Transition(float thickness, Color color)
            {
                Thickness = thickness;
                Brush = new SolidBrush(color);
            }
        }

        public float Thickness { get; private set; }
        public bool DrawIndicatorsOnRim { get; private set; }
        public bool IndicatorsOnBothSides { get; private set; }

        public Transition InnerTransition { get; private set; }
        public Transition OuterTransition { get; private set; }

        public static BoardThemeRimConfig FromJson(JsonValue json)
        {
            var config = new BoardThemeRimConfig();

            config.Thickness = JsonValueReader.ReadPercentToFloat01(json["thickness"]);

            if (json.ContainsKey("indicators"))
            {
                config.DrawIndicatorsOnRim = true;

                var indicatorsJson = json["indicators"];
                config.IndicatorsOnBothSides = indicatorsJson["indicators_on_both_sides"];
            }
            else
            {
                config.DrawIndicatorsOnRim = false;
            }

            if (json.ContainsKey("inner_transition"))
            {
                var transitionJson = json["inner_transition"];
                config.InnerTransition = new Transition(
                    JsonValueReader.ReadPercentToFloat01(transitionJson["thickness"]),
                    ColorTranslator.FromHtml(transitionJson["color"])
                    );
            }

            if (json.ContainsKey("outer_transition"))
            {
                var transitionJson = json["outer_transition"];
                config.OuterTransition = new Transition(
                    JsonValueReader.ReadPercentToFloat01(transitionJson["thickness"]),
                    ColorTranslator.FromHtml(transitionJson["color"])
                    );
            }

            return config;
        }
    }
}
