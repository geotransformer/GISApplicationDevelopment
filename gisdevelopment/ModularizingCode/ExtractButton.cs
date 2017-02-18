using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Collections;
namespace ModularizingCode
{
    public class ExtractButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ExtractButton()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
            extract_river_Subsets();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        public void extract_river_Subsets()
        {
            //****** Author:  Jim Detwiler, edited by Frank Hardisty
            //******** Date:  6/Nov/2009
            //* Description:  Searches the active data frame for a layer called
            //*               "U.S. Rivers" or "us_hydro".  When found, creates a new
            //*               collection of river names to pass to another sub
            //*               that creates a new shapefile for each individual
            //*               river.
            //**** Calls to:  Util_Extract
            //**** Calls by:
            //***** Globals:
            //****** Locals:  pMxDoc, pMap, pLayers, pLayer, colRivers,
            //******          strQueryField, i
            //**** Location:
            //****** Source:
            //******* Notes:
            //****************************************
            //* Revision Author:  Andrew Murdoch
            //*** Revision Date:  5/28/2011
            //** Revision Notes:  Updated for ArcGIS 10 and VB.NET
            //*** Revision Date:  11/8/2014
            //** Revision Notes:  Updated for C#

            //* Revision Author:  Vincent
            //*** Revision Date:  1/10/2017
            //** Revision Notes:  Updated for ArcGIS 10.3 
           
            // Getting the mxd
            IMxDocument pMxDoc = default(IMxDocument);
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            // Getting the focus map from the mxd
            IMap pMap = pMxDoc.FocusMap;

            // Getting an enumeration of layers from the map
            IEnumLayer pLayers = pMap.Layers;

            // Moving the pointer to the first layer
            ILayer pLayer = pLayers.Next();           

            // Looping through all feature layers
            while (!(pLayer == null))
            {
                // the name of river system may be changed to "U. S. River" ( the original is us_hydro)
                if (pLayer.Name == "U.S. Rivers" || pLayer.Name == "us_hydro")
                {
                     // Found the layer we want
                    //MessageBox.Show("find U.S. Rivers layer", "Warning", MessageBoxButtons.OK); 
                    break;
                   
                }
                // If not correct layer, go to next layer
                pLayer = pLayers.Next();
                
            }

            if (pLayer == null)
            {
                // Couldn't find the layer.  Tell user, then quit.
                MessageBox.Show("Sorry, can't find U.S. Rivers layer", "Warning", MessageBoxButtons.OK);
                return;
            }

            // Creating a new generic <string> river name collection
            //Using List<T> instead of ArrayList for generic collection
            //ArrayList tesmp = new ArrayList(){"s", "d"};
            var colRivers = new List<string>();
           

            //Adding items to the collection
            colRivers.Add("Colorado River");
            colRivers.Add("Columbia River");
            colRivers.Add("Mississippi River");
            colRivers.Add("Rio Grande");

            // This is the field to query on
            string strQueryField = "NAMEEN";

            int i;
            // For each item in the collection
            for (i = 0; i < colRivers.Count; i++)
            {
                // Make call to extract passing the layer, current item in
                // collection, and the query field
               Utilities.extract((IFeatureLayer)pLayer, colRivers[i], strQueryField);
            }
            //prompt the user the extract work is done
            MessageBox.Show("Finished creating river subsets.", "Done in button", MessageBoxButtons.OK);
        }

    }

}
