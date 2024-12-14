import {signal} from "@angular/core";
import {Subject} from "rxjs";

import {FormMode} from "../models/form-mode.enum";
import {Command} from "../models/command.model";

export class OpCom {
  formMode = signal<FormMode>(FormMode.Display);
  toParent: Subject<Command> = new Subject<Command>();
  toChildren: Subject<Command> = new Subject<Command>();
  // toChildrenQueue: Queue<Command> = new Queue<Command>(50);

}
