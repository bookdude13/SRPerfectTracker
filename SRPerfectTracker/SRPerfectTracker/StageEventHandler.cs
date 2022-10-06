using SRModCore;
using Synth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace SRPerfectTracker
{
    internal class StageEventHandler
    {
        private SRLogger logger;
        private IStageEvents listener;
        private GameControlManager gameControlManager = null;

        public StageEventHandler(SRLogger logger, IStageEvents eventListener)
        {
            this.logger = logger;
            this.listener = eventListener;
        }

        public void SetupEvents(GameControlManager gcm)
        {
            if (gameControlManager == gcm)
            {
                logger.Msg($"GCM matches. Null? {gcm == null}");
                return;
            }

            if (gcm == null)
            {
                logger.Msg("GCM null, skipping setup");
                return;
            }

            gameControlManager = gcm;
            try
            {
                logger.Msg("Adding stage events!");
                StageEvents stageEvents = new StageEvents();
                stageEvents.OnSongStart = new UnityEvent();
                stageEvents.OnSongStart.AddListener(listener.OnSongStart);
                stageEvents.OnSongEnd = new UnityEvent();
                stageEvents.OnSongEnd.AddListener(listener.OnSongEnd);
                stageEvents.OnNoteHit = new UnityEvent();
                stageEvents.OnNoteHit.AddListener(listener.OnNoteHit);
                stageEvents.OnNoteFail = new UnityEvent();
                stageEvents.OnNoteFail.AddListener(listener.OnNoteFail);
                stageEvents.OnEnterSpecial = new UnityEvent();
                stageEvents.OnEnterSpecial.AddListener(listener.OnEnterSpecial);
                stageEvents.OnCompleteSpecial = new UnityEvent();
                stageEvents.OnCompleteSpecial.AddListener(listener.OnCompleteSpecial);
                stageEvents.OnFailSpecial = new UnityEvent();
                stageEvents.OnFailSpecial.AddListener(listener.OnFailSpecial);

                GameControlManager.UpdateStageEventList(stageEvents);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
            }
        }
    }
}
