export interface Action {
    actionType: ActionType,
    param: any,
    panelID?: string,
    sectionID?: string,
    controlID?: string
}

export enum ActionType {
    Submit,
    SubmitPanel,
    SubmitSection,
    SubmitControl,
    Delete,
    DeletePanel,
    DeleteSection,
    DeleteControl,
    CollapseLink
}