using System;
using System.Collections.Generic;
using System.Linq;

namespace CardPreQualificationTool.Models
{
    // Holds details of _decisions made about customers' credit card applications
    public class DecisionRepository
    {
        private readonly Dictionary<Guid, Decision> _decisions;

        private DateTime _lastPruned;

        // How often we check for old _decisions that can be removed
        private const int PRUNE_FREQUENCY_MINUTES = 1;

        // How long the _decisions remain in the repository
        private const int PERSISTENCE_DURATION_MINUTES = 15;

        public DecisionRepository()
        {
            _decisions = new Dictionary<Guid, Decision>();
            _lastPruned = DateTime.Now;
        }

        public Guid Add(Decision decision)
        {
            Guid requestId;

            do
            {
                requestId = Guid.NewGuid();
            } while (_decisions.ContainsKey(requestId));

            _decisions.Add(requestId, decision);
            return requestId;
        }

        public Decision Get(Guid requestId)
        {
            // Periodically remove old entries so the repository doesn't grow indefinitely
            if (DateTime.Now >= _lastPruned.AddMinutes(PRUNE_FREQUENCY_MINUTES))
            {
                DateTime removeBefore = DateTime.Now.AddMinutes(-PERSISTENCE_DURATION_MINUTES);

                foreach (var item in _decisions.Where(kvp => kvp.Value.Timestamp < removeBefore).ToList())
                {
                    _decisions.Remove(item.Key);
                }

                _lastPruned = DateTime.Now;
            }

            return _decisions.GetValueOrDefault(requestId);
        }
    }
}
