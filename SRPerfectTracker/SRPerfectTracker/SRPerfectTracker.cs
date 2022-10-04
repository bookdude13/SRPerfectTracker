using MelonLoader;
using SRModCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPerfectTracker
{
    public class SRPerfectTracker : MelonMod
    {
        public static SRPerfectTracker Instance { get; private set; }

        private SRLogger logger;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            logger = new MelonLoggerWrapper(LoggerInstance);
            Instance = this;
        }
    }
}
