import { FormGroup } from "@angular/forms";
import { WebPage } from "@sloth-http";

export interface DynamicForm {
    pageForm: FormGroup | undefined;
    config: WebPage;
}