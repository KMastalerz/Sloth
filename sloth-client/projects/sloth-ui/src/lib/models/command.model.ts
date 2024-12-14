import {ActionType} from "./action-type.enum";
import {TargetType} from "./target-type.enum";

export interface Command {
  actionType: ActionType,
  params: unknown,
  targetName: string,
  targetType: TargetType,
}
