using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
namespace ObjectCollection
{
    /// <summary>
    /// This add-in project makes all the layers visible 
    /// </summary>
    public class Projec01 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Projec01()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
            all_layers_visible();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        private void all_layers_visible()         
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap = pMxDoc.FocusMap;

            IEnumLayer pEnumLayer = pMap.Layers;

            ILayer pLayer = pEnumLayer.Next();

            while (pLayer != null)
            {
                pLayer.Visible = true;
                pLayer = pEnumLayer.Next();
            }

            pMxDoc.UpdateContents();


            /*
             * pMxDoc.ActiveView.Refresh();
             */
            
            IActiveView pActiveView = pMxDoc.ActiveView;
            pActiveView.Refresh();

        }
    }

}
