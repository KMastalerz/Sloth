import { Injectable } from '@angular/core';
import { findIconDefinition, IconDefinition, IconName, IconPrefix, library } from '@fortawesome/fontawesome-svg-core';
import { faChartSimple, faMessage, faPlus } from "@fortawesome/free-solid-svg-icons";
import { faMessage as farMessage } from "@fortawesome/free-regular-svg-icons";
@Injectable({
  providedIn: 'root'
})
export class IconLibraryService {

  constructor() {
    library.add(
      faMessage,
      faChartSimple,
      faPlus
    )

    library.add(
      farMessage
    )
   }

  getIcon(prefix: IconPrefix, iconName: IconName): IconDefinition {
    const iconLookup = { prefix: prefix, iconName: iconName };
    const icon = findIconDefinition(iconLookup);

    if(icon) return icon;

    //default to fas plus
    return faPlus;
  }
}
