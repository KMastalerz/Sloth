export interface Action {
    actionType: ActionType,
    param: any
}

export enum ActionType {
    Submit,
    SubmitPanel,
    SubmitControl,
    CollapseLink
}