# Virtual Suspect
## Message Structure
The questions and answers used to interact with the virtual suspect are structured using XML. Inside the structure is the original speech related to the parsed data.

A question can be an yes or no question (*yes-no*) or a information gathering question (*get-information*), each one has different porpuses and they are described on Question Frame Structure. A question has a focus, it describes what is the type of data that the question should retrieve. Some special focus are described bellow (count, equal). The data retrieve should satisfy conditions, that have a dimension, an operator and values. 

A answer contains the structured question it is answering, and the response to that question. Each response has its dimension
and the value. 

### Question Frame Structure
A question frame can have the following types:

1. *yes-no*: 
The question will be used to validate information. 
Returns a positive boolean value if there is any data in the knowledge base satisfying the question's restriction. Returns a false boolean value otherwise.

2. *get-information*: 
The question will be used to gather information. 
Returns the data that satisfies the question's restrictions. Uses the Question focus to select specific types of data.

The question's focus can be one of the dimensions or a special focus:

1. *action* (dimension)
2. *time* (dimension)
3. *location* (dimension)
4. *agent* (dimension)
5. *theme* (dimension)
6. *manner* (dimension)
7. *reason* (dimension)
8. *count* (retrieves the number of responses that satisfies the query)
9. *distinct* (retieves the responses without duplicates)

Each condition has:

1. *dimension* 
2. *operator*: the operator used to test the condition
3. *value*: the value to be satisfied by the operator. Each condition can have multiple values.
4. *speech*: the original sentence 

### Question Frame Examples
##### Question 1: 
> "Onde comeu no dia 21 de Janeiro de 2016?"

```
<question>
	<speech>"Onde comeu no dia 21 de Janeiro de 2016?"</speech>
	<type>get-information</type>
	<focus>
		<dimension>location</dimension>
		<speech>"Onde"</speech>
	</focus>
	<condition>
		<dimension>action</dimension>
		<operator>equal</operator>
		<value>Comer</value>
		<speech>"comeu"</speech>
	</condition>
	<condition>
		<dimension>time</dimension>
		<operator>between</operator>
		<begin>21/01/2016 00:00:00</begin>
		<end>21/01/2016 23:59:59</end>
		<speech>"no dia 21 de Janeiro de 2016"</speech>
	</condition>
</question>
```
##### Question 2: 
> "Com quem foi no dia 21 de Janeiro de 2016 ao Café Sol?"
	
```
<question>
	<speech>"Com quem foi no dia 21 de Janeiro de 2016 ao Café Sol?"</speech>
	<type>get-information</type>
	<focus>
		<dimension>agent</dimension>
		<speech>"Com quem"</speech>
	</focus>
	<condition>
		<dimension>action</dimension>
		<operator>equal</operator>
		<value>Ir</value>
		<speech>"foi"</speech>
	</condition>
	<condition>
		<dimension>time</dimension>
		<operator>between</operator>
		<begin>21/01/2016 00:00:00</begin>
		<end>21/01/2016 23:59:59</end>
		<speech>"no dia 21 de Janeiro de 2016"</speech>
	</condition>
	<condition>
		<dimension>location</dimension>
		<operator>equal</operator>
		<value>Café Sol</value>	
		<speech>"ao Café Sol"</speech>
	</condition>
</question>
```
### Answer Frame Structure

The answer contains the question proposed and each response that satisfies it. 

Each response contains:

1. dimension*: the dimension of the response
2. *value*: the actual value of the response 

### Answer Frame Examples
##### Answer to Question 1:
```
<answer>
	<question>
		<speech>"Onde comeu no dia 21 de Janeiro de 2016?"</speech>
		<type>get-information</type>
		<focus>
			<dimension>location</dimension>
			<speech>"Onde"</speech>
		</focus>
		<condition>
			<dimension>action</dimension>
			<operator>equal</operator>
			<value>Eat</value>
			<speech>"comeu"</speech>
		</condition>
		<condition>
			<dimension>time</dimension>
			<operator>between</operator>
			<begin>21/01/2016 00:00:00</begin>
			<end>21/01/2016 23:59:59</end>
			<speech>"no dia 21 de Janeiro de 2016"</speech>
		</condition>
	</question>	
	<response>
		<dimension>location</dimension>
		<value>Restaurante Girassol</value>
		<cardinality>1</cardinality>	
	</response>
	<response>
		<dimension>location</dimension>
		<value>Café Sol</value>
		<cardinality>2</cardinality>
	</response>
</answer>
```
##### Answer to Question 2: 
```
<answer>
	<question>
		<speech>"Com quem foi no dia 21 de Janeiro de 2016 ao Café Sol?"</speech>
		<type>get-information</type>
		<focus>
			<dimension>agent</dimension>
			<speech>"Com quem"</speech>
		</focus>
		<condition>
			<dimension>action</dimension>
			<operator>equal</operator>
			<value>Ir</value>
			<speech>"foi"</speech>
		</condition>
		<condition>
			<dimension>time</dimension>
			<operator>between</operator>
			<begin>21/01/2016 00:00:00</begin>
			<end>21/01/2016 23:59:59</end>
			<speech>"no dia 21 de Janeiro de 2016"</speech>
		</condition>
		<condition>
			<dimension>location</dimension>
			<operator>equal</operator>
			<value>Café Sol</value>	
			<speech>"ao Café Sol"</speech>
		</condition>
	</question>
	<response>
		<dimension>agent</dimension>
		<value>Eu</value>
		<cardinality>1</cardinality>
	</response>
	<response>
		<dimension>agent</dimension>
		<value>Pedro Pereira</value>
		<cardinality>1</cardinality>
	</response>
</answer>
```
