export interface Action {
    actionType: ActionType,
    param: any
}

export enum ActionType {
    Add,
    Delete,
    Update,
    Process,
    Collapse
}