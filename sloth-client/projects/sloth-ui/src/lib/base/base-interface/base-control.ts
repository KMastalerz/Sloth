export interface BaseControl {
    readConfig: () => void | Promise<void>;
    ngOnInit:() =>  void | Promise<void>;
}