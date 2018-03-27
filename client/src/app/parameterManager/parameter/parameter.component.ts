import {Component, EventEmitter, Input, OnInit} from '@angular/core';
import {Parameter} from '../parameterEntity';
import {ParameterService} from '../../Services/parameterService';
import {Subject} from 'rxjs/Subject';

@Component({
	selector: 'cmi-viaduc-parameter',
	templateUrl: './parameter.component.html',
	styleUrls: ['./parameter.component.less']
})
export class ParameterComponent implements OnInit {
	@Input()
	public parameter: Parameter;
	@Input()
	public validationEvent: EventEmitter<void> = new EventEmitter<void>();

	public active: boolean = false;
	public value: string;
	public checked: boolean;
	public validationError: boolean;
	private static _onFocusChange: Subject<void> = new Subject();

	constructor (private _paramService: ParameterService) {
	}

	public ngOnInit() {
		ParameterComponent._onFocusChange.subscribe(() => {
				this.cancelEdit();
		});
		this.validationEvent.subscribe(() => {
			this.validationError = !this._isValid();
		});
		this.value = this.parameter.value;
		this.checked = this.parameter.value === 'True';
	}

	public onValueChanged(event: any) {
		if (this.parameter.type === 'checkbox') {
			this.checked = event.target.checked;
			if (this.checked === (this.parameter.value === 'True')) {
				this.active = false;
			}
		} else {
			this.value = event.target.value;
		}
	}

	public onFocus() {
		ParameterComponent._onFocusChange.next();
		this.active = true;
	}

	public saveParameter() {
		if (this.parameter.type === 'checkbox') {
			this.parameter.value = this.checked.toString();
		} else {
			this.parameter.value = this.value;
		}
		this.validationError = !this._isValid();
		if (!this.validationError) {
			this._paramService.saveParameter(this.parameter).then( success => this.validationError = !success);
		}
	}

	public cancelEdit() {
		this.value = this.parameter.value;
		if (this.parameter.type === 'checkbox') {
			this.checked = this.parameter.value === 'True';
		} else {
			this.value = this.parameter.value;
		}
		this.active = false;
	}

	private _isValid(): boolean {
		if (this.parameter && this.parameter.regexValidation && this.parameter.value) {
			let matches = this.parameter.value.match(this.parameter.regexValidation);
			return !!(matches && matches[0] !== null);
		} else {
			return true;
		}
	}

	public getClass(): string {
		return this.validationError ? 'parameter-list row alert-danger' : 'parameter-list row';
	}
}
