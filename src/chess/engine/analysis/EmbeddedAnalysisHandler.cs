using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_pos_db_gui.src.chess.engine.analysis
{
    public abstract class EmbeddedAnalysisHandler
    {
        public abstract System.Windows.Forms.Panel PrepareAndGetEmbeddedAnalysisPanel();

        public abstract void OnEmbeddedAnalysisEnded();

        public abstract void OnEmbeddedAnalysisStarted(EngineAnalysisForm form);

        public abstract void Dispose();
    }
}
