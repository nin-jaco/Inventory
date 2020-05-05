var ViewModel = function() {

	var self = this;
	self.Id = ko.observable();
	self.TypeList = ko.observableArray(['Printer', 'Screen', 'Laptop', 'Keyboard', 'Mouse']);
	self.Description = ko.observable();
	self.SerialNumber = ko.observable();
	self.ImageUrl = ko.observable();
	self.PurchasePrice = ko.observable();

	self.hardwareList = ko.observableArray([]);

	var hardwareUri = 'api/Hardware';

	function ajaxFunction(uri, method, data) {

		//self.errorMessage('');

		return $.ajax({
			type: method,
			url: uri,
			dataType: 'json',
			contentType: 'application/json',
			data: data ? JSON.stringify(data) : null
		}).fail(function(jqXHR, textStatus, errorThrown) {
			alert('Error ' + errorThrown);
		});
		
	}

	//clear form
	self.clearFields = function clearFields() {
		self.Type('');
		self.Description('');
		self.SerialNumber('');
		self.ImageUrl('');
		self.PurchasePrice('0');
	}

	//add new hardware item
	self.addHardware = function addHardware(newHardware) {
		var hardwareObject = {
			Id: self.Id(),
			Type: self.Type(),
			Description: self.Description(),
			SerialNumber: self.SerialNumber(),
			ImageUrl: self.ImageUrl(),
			PurchasePrice: self.PurchasePrice()
		};
		ajaxFunction(hardwareUri, 'POST', hardwareObject).done(function() {
			self.clearFields();
			alert("Hardware item added successfully!");
			getHardwareList();
		})
	}

	//get hardware item list
	function getHardwareList() {
		$("div.loadingZone").show();
		ajaxFunction(hardwareUri, 'GET').done(function(data) {
			$("div.loadingZone").hide();
			self.hardwareList(data);
		});
	}

	//get hardware item
	self.hardwareDetail = function(selectedHardwareItem) {
		self.Id(selectedHardwareItem.Id);
		self.Type(selectedHardwareItem.Type);
		self.Description(selectedHardwareItem.Description);
		self.SerialNumber(selectedHardwareItem.SerialNumber);
		self.ImageUrl(selectedHardwareItem.ImageUrl);
		self.PurchasePrice(selectedHardwareItem.PurchasePrice);

		$('#Save').hide();
		$('#Clear').hide();

		$('#Update').show();
		$('#Cancel').show();
	};

	self.cancel = function() {
		self.clearFields();

		$('#Save').show();
		$('#Clear').show();

		$('#Update').hide();
		$('#Cancel').hide();
	}

	//update hardware item
	self.updateHardwareItem = function() {
		var hardwareObject = {
			Id: self.Id(),
			Type: self.Type(),
			Description: self.Description(),
			SerialNumber: self.SerialNumber(),
			ImageUrl: self.ImageUrl(),
			PurchasePrice: self.PurchasePrice()
		};
		ajaxFunction(hardwareUri + self.Id(), 'PUT', hardwareObject).done(function () {
			alert('Hardware item updated successfully !');
			getHardwareList();
			self.cancel();
		});
	}

	//delete hardware item
	self.deleteHardwareItem = function(selectedHardwareItem) {
		ajaxFunction(hardwareUri + selectedHardwareItem.Id, 'DELETE').done(function() {

			alert('Hardware Item Deleted Successfully');
			getHardwareList();
		});
	}

	getHardwareList();
};

ko.applyBindings(new ViewModel());