using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualSuspect;

namespace VirtualSupectQuestionAnswering
{
    public partial class DataVisualizer : Form{

        KnowledgeBase suspectKB;

        public DataVisualizer(KnowledgeBase kb) {
            InitializeComponent();
            this.suspectKB = kb;
        }

        private void DataVisualizer_Load(object sender, EventArgs e) {

            dgvEntities.Columns.Add("id", "ID");
            dgvEntities.Columns.Add("type", "Type");
            dgvEntities.Columns.Add("value", "Value");
            dgvEntities.Columns.Add("incriminatory", "Incriminatory");
            dgvEntities.Columns.Add("known", "Known");

            dgvActions.Columns.Add("id","ID");
            dgvActions.Columns.Add("action","Action");

            dgvEvents.Columns.Add("id", "ID");
            dgvEvents.Columns.Add("incriminatory", "Incriminatory");
            dgvEvents.Columns.Add("action", "Action");
            dgvEvents.Columns.Add("time", "Time");
            dgvEvents.Columns.Add("location", "Location");
            dgvEvents.Columns.Add("theme", "Theme");
            dgvEvents.Columns.Add("agent", "Agent");
            dgvEvents.Columns.Add("manner", "Manner");
            dgvEvents.Columns.Add("reason", "Reason");

            //Get Actions
            foreach(ActionNode action in suspectKB.Actions) {
                dgvActions.Rows.Add( new string[] { "" + action.ID , action.Action});
            }

            //Get Entities
            foreach (EntityNode entity in suspectKB.Entities) {
                dgvEntities.Rows.Add(new string[] { "" + entity.ID, entity.Type, entity.Value, "" + entity.Incriminatory, "" + entity.Known});
            }

            //Get Actions
            foreach (EventNode eventNode in suspectKB.Events) {
                dgvEvents.Rows.Add(new string[] {
                    "" + eventNode.ID,
                    "" + eventNode.Incriminatory,
                    "' " +eventNode.Action.Action + " ' ( " + eventNode.Action.ID + " )",
                    "' " +eventNode.Time.Value+ " ' ( " + eventNode.Time.ID + " )",
                    "' " + eventNode.Location.Value+ " ' ( " + eventNode.Location.ID + " )",
                    ConvertToString(eventNode.Theme),
                    ConvertToString(eventNode.Agent),
                    ConvertToString(eventNode.Manner),
                    ConvertToString(eventNode.Reason),
                });
            }

        }

        private string ConvertToString(List<EntityNode> nodes) {

            string resultString = "{ ";

            for (int i = 0; i < nodes.Count; i++) {
                resultString += "' " + nodes[i].Value + " '( " + nodes[i].ID + " )";

                if (i < nodes.Count - 1) {
                    resultString += " , ";
                }

            }
            return resultString + " }";
        }
    }
}
