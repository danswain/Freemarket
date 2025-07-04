Feature: Basket Feature
As a happy shopper
I would like to be able to use a shopping basket
So that I can buy the items I have added to the basket

Scenario: Add an item to the basket
Given I have a basket
When I add an item to the basket
Then I have 1 item in the basket


Scenario: Add multiple items to the basket
 Given I have a basket
 When I add an item to the basket
 When I add an item to the basket
 Then I have 2 item in the basket
 
# Remove an item from the basket
# Add multiple of the same item to the basket
# Get the total cost for the basket (including 20% VAT)
# Get the total cost without VAT
# Add a discounted item to the basket
# Add a discount code to the basket (excluding discounted items)
# Add shipping cost to the UK
# Add shipping cost to other countries